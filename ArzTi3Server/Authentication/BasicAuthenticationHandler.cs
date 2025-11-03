using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ArzSwDbContext _arzSwDbContext;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ArzSwDbContext arzSwDbContext)
            : base(options, logger, encoder, clock)
        {
            _arzSwDbContext = arzSwDbContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                AuthenticationHeaderValue authHeader;
                
                try
                {
                    authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]!);
                }
                catch
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }
                
                if (!authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                
                if (string.IsNullOrEmpty(authHeader.Parameter))
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                
                if (credentials.Length != 2)
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                
                var username = credentials[0];
                var password = credentials[1];

                var user = await _arzSwDbContext.ArzswBenutzers
                    .Include(u => u.ArzswMandant)
                    .Include(u => u.ArzswDatenbank)
                    .FirstOrDefaultAsync(u => u.LoginName == username);

                if (user == null)
                    return AuthenticateResult.Fail("Invalid Username");

                if (!VerifyPassword(password, user.LoginPasswort ?? "", user.LoginPasswortCrypt ?? ""))
                    return AuthenticateResult.Fail("Invalid Password");

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ArzswBenutzerId.ToString()),
                    new Claim(ClaimTypes.Name, user.LoginName),
                    new Claim("MandantId", user.ArzswMandantId.ToString()),
                    new Claim("ClientCode", user.ArzswMandant.CodeKenner),
                    new Claim("ConnectionString", user.ArzswDatenbank.DatenbankConnectionString)
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers.Append("WWW-Authenticate", "Basic realm=\"ArzTi3 API\", charset=\"UTF-8\"");
            return base.HandleChallengeAsync(properties);
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.Headers.Append("WWW-Authenticate", "Basic realm=\"ArzTi3 API\", charset=\"UTF-8\"");
            return base.HandleForbiddenAsync(properties);
        }

        private bool VerifyPassword(string providedPassword, string storedPassword, string storedPasswordCrypt)
        {
            if (!string.IsNullOrEmpty(storedPasswordCrypt))
            {
                return BCrypt.Net.BCrypt.Verify(providedPassword, storedPasswordCrypt);
            }
            return providedPassword == storedPassword;
        }
    }
}