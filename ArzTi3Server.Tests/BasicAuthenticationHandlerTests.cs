using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Xunit;
using Moq;
using ArzTi3Server.Authentication;
using Microsoft.EntityFrameworkCore;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Tests
{
    public class BasicAuthenticationHandlerTests : IDisposable
    {
        private readonly Mock<IOptionsMonitor<AuthenticationSchemeOptions>> _optionsMock;
        private readonly Mock<ILoggerFactory> _loggerFactoryMock;
        private readonly Mock<UrlEncoder> _urlEncoderMock;
        private readonly ArzSwDbContext _context;
        private readonly BasicAuthenticationHandler _handler;

        public BasicAuthenticationHandlerTests()
        {
            _optionsMock = new Mock<IOptionsMonitor<AuthenticationSchemeOptions>>();
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _urlEncoderMock = new Mock<UrlEncoder>();

            var options = new DbContextOptionsBuilder<ArzSwDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ArzSwDbContext(options);

            _optionsMock.Setup(o => o.Get("BasicAuthentication"))
                .Returns(new AuthenticationSchemeOptions());

            var loggerMock = new Mock<ILogger<BasicAuthenticationHandler>>();
            _loggerFactoryMock.Setup(f => f.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);

            _handler = new BasicAuthenticationHandler(
                _optionsMock.Object,
                _loggerFactoryMock.Object,
                _urlEncoderMock.Object,
                new Mock<ISystemClock>().Object,
                _context
            );
        }

        [Fact]
        public async Task HandleAuthenticateAsync_MissingAuthorizationHeader_ReturnsFailure()
        {
            var context = new DefaultHttpContext();
            await _handler.InitializeAsync(new AuthenticationScheme("BasicAuthentication", null, typeof(BasicAuthenticationHandler)), context);

            var result = await _handler.AuthenticateAsync();

            Assert.False(result.Succeeded);
            Assert.Equal("Missing Authorization Header", result.Failure?.Message);
        }

        [Fact]
        public async Task HandleAuthenticateAsync_InvalidAuthorizationHeader_ReturnsFailure()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["Authorization"] = "InvalidHeader";
            await _handler.InitializeAsync(new AuthenticationScheme("BasicAuthentication", null, typeof(BasicAuthenticationHandler)), context);

            var result = await _handler.AuthenticateAsync();

            Assert.False(result.Succeeded);
            Assert.Equal("Invalid Authorization Header", result.Failure?.Message);
        }

        [Fact]
        public async Task HandleAuthenticateAsync_InvalidUsername_ReturnsFailure()
        {
            var context = new DefaultHttpContext();
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("invalid:password"));
            context.Request.Headers["Authorization"] = $"Basic {credentials}";
            await _handler.InitializeAsync(new AuthenticationScheme("BasicAuthentication", null, typeof(BasicAuthenticationHandler)), context);

            var result = await _handler.AuthenticateAsync();

            Assert.False(result.Succeeded);
            Assert.Equal("Invalid Username", result.Failure?.Message);
        }

        [Fact]
        public async Task HandleAuthenticateAsync_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var mandant = new ArzswMandant {
                ArzswMandantId = 1,
                CodeKenner = "TEST001",
                MandantName = "Test Mandant"
            };
            var datenbank = new ArzswDatenbank {
                ArzswDatenbankId = 1,
                DatenbankName = "Test Database",
                DatenbankConnectionString = "Server=test;Database=test;",
                DatenbankAktiv = true,
                ArzswMandantId = 1,
                ArzswMandant = mandant
            };
            var user = new ArzswBenutzer
            {
                ArzswBenutzerId = 1,
                BenutzerName = "Test User",
                LoginName = "testuser",
                LoginPasswort = "password123",
                ArzswMandantId = 1,
                ArzswDatenbankId = 1,
                ArzswMandant = mandant,
                ArzswDatenbank = datenbank
            };

            await _context.ArzswMandants.AddAsync(mandant);
            await _context.ArzswDatenbanks.AddAsync(datenbank);
            await _context.ArzswBenutzers.AddAsync(user);
            await _context.SaveChangesAsync();

            var context = new DefaultHttpContext();
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("testuser:password123"));
            context.Request.Headers["Authorization"] = $"Basic {credentials}";
            await _handler.InitializeAsync(new AuthenticationScheme("BasicAuthentication", null, typeof(BasicAuthenticationHandler)), context);

            // Act
            var result = await _handler.AuthenticateAsync();

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("testuser", result.Principal?.Identity?.Name);
            Assert.Equal("TEST001", result.Principal?.FindFirst("ClientCode")?.Value);
            Assert.Equal("Server=test;Database=test;", result.Principal?.FindFirst("ConnectionString")?.Value);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}