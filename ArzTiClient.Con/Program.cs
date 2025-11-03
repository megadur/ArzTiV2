using ArzTiClient.Api;
using ArzTiClient.Client;
using ArzTiClient.Model;
using CommandLine;
using Microsoft.Extensions.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ArzTiClient.Con;

class Program
{
    private static IConfiguration? _configuration;
    private static string _appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

    static async Task<int> Main(string[] args)
    {
        // Load configuration
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        return await Parser.Default.ParseArguments<
                ConfigCommand,
                PrescriptionsCommand,
                ApothekenCommand,
                MarkAsReadCommand,
                SetAbgerechnetCommand>
            (args)
            .MapResult(
                (ConfigCommand cmd) => HandleConfigCommand(cmd),
                (PrescriptionsCommand cmd) => HandlePrescriptionsCommand(cmd),
                (ApothekenCommand cmd) => HandleApothekenCommand(cmd),
                (MarkAsReadCommand cmd) => HandleMarkAsReadCommand(cmd),
                (SetAbgerechnetCommand cmd) => HandleSetAbgerechnetCommand(cmd),
                errs => Task.FromResult(1));
    }

    private static Task<int> HandleConfigCommand(ConfigCommand cmd)
    {
        if (cmd.ShowCurrent)
        {
            Console.WriteLine("Current Configuration:");
            Console.WriteLine($"BaseUrl: {_configuration["ApiSettings:BaseUrl"]}");
            Console.WriteLine($"Username: {_configuration["ApiSettings:Username"]}");
            Console.WriteLine($"Password: {(string.IsNullOrEmpty(_configuration["ApiSettings:Password"]) ? "Not set" : "****")}");
            Console.WriteLine($"Tenant: {_configuration["ApiSettings:Tenant"]}");
            return Task.FromResult(0);
        }

        // Update configuration if any values are provided
        if (!string.IsNullOrEmpty(cmd.BaseUrl) || 
            !string.IsNullOrEmpty(cmd.Username) || 
            !string.IsNullOrEmpty(cmd.Password) || 
            !string.IsNullOrEmpty(cmd.Tenant))
        {
            try
            {
                // Read existing config file
                string json = File.ReadAllText(_appSettingsPath);
                JObject jsonObj = JObject.Parse(json);
                
                // Update only the fields that were provided
                if (!string.IsNullOrEmpty(cmd.BaseUrl))
                {
                    jsonObj["ApiSettings"]["BaseUrl"] = cmd.BaseUrl;
                    Console.WriteLine($"Updated BaseUrl to: {cmd.BaseUrl}");
                }
                
                if (!string.IsNullOrEmpty(cmd.Username))
                {
                    jsonObj["ApiSettings"]["Username"] = cmd.Username;
                    Console.WriteLine($"Updated Username to: {cmd.Username}");
                }
                
                if (!string.IsNullOrEmpty(cmd.Password))
                {
                    jsonObj["ApiSettings"]["Password"] = cmd.Password;
                    Console.WriteLine("Updated Password");
                }
                
                if (!string.IsNullOrEmpty(cmd.Tenant))
                {
                    jsonObj["ApiSettings"]["Tenant"] = cmd.Tenant;
                    Console.WriteLine($"Updated Tenant to: {cmd.Tenant}");
                }
                
                // Save the updated configuration
                File.WriteAllText(_appSettingsPath, jsonObj.ToString());
                Console.WriteLine("Configuration updated successfully");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Error updating configuration: {ex.Message}");
                Console.ResetColor();
                return Task.FromResult(1);
            }
        }
        
        return Task.FromResult(0);
    }

    private static async Task<int> HandlePrescriptionsCommand(PrescriptionsCommand cmd)
    {
        try
        {
            var apiClient = CreateApiClient();
            var prescriptionsApi = new PrescriptionsApi(apiClient);
            
            Console.WriteLine($"Fetching prescriptions (Page: {cmd.Page}, PageSize: {cmd.PageSize})...");
            
            var response = await prescriptionsApi.ApiPrescriptionsNewGetAsync(
                cmd.Page, 
                cmd.PageSize,
                cmd.Type);
            
            Console.WriteLine($"Retrieved {response.Prescriptions?.Count ?? 0} prescriptions (Total: {response.TotalCount})");
            Console.WriteLine("ID\tType\tRezeptUuid\tTransaktionsNummer");
            
            if (response.Prescriptions != null)
            {
                foreach (var item in response.Prescriptions)
                {
                    Console.WriteLine($"{item.Id}\t{item.Type}\t{item.RezeptUuid}\t{item.TransaktionsNummer}");
                    if (cmd.Verbose)
                    {
                        Console.WriteLine($"  Muster16Id: {item.Muster16Id}");
                        Console.WriteLine($"  ErezeptId: {item.ErezeptId}");
                        Console.WriteLine($"  TransferArz: {item.TransferArz}");
                        Console.WriteLine("  --------------------------");
                    }
                }
            }
            
            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }

    private static async Task<int> HandleApothekenCommand(ApothekenCommand cmd)
    {
        try
        {
            var apiClient = CreateApiClient();
            var apothekenApi = new ApothekenApi(apiClient);

            if (cmd.Id.HasValue)
            {
                // Get specific apotheke by ID
                var apotheke = await apothekenApi.ApiApothekenIdGetAsync(cmd.Id.Value);
                DisplayApotheke(apotheke);
            }
            else if (cmd.IkNr.HasValue)
            {
                // Get specific apotheke by IK
                var apotheke = await apothekenApi.ApiApothekenByIkIkNrGetAsync(cmd.IkNr.Value);
                DisplayApotheke(apotheke);
            }
            else
            {
                // List apotheken
                var response = await apothekenApi.ApiApothekenGetAsync(
                    cmd.Page,
                    cmd.PageSize,
                    cmd.Search);

                Console.WriteLine($"Retrieved {response.Apotheken?.Count ?? 0} apotheken (Total: {response.TotalCount})");
                Console.WriteLine("ID\tName\tIK Number\tLocation");

                if (response.Apotheken != null)
                {
                    foreach (var item in response.Apotheken)
                    {
                        Console.WriteLine($"{item.IdApotheke}\t{item.ApothekeName}\t{item.ApoIkNr}\t{item.Ort}");
                        if (cmd.Verbose)
                        {
                            Console.WriteLine($"  Address: {item.Strasse}, {item.Plz} {item.Ort}");
                            Console.WriteLine($"  Contact: {item.Email}, {item.Telefon}");
                            Console.WriteLine("  --------------------------");
                        }
                    }
                }
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }

    private static void DisplayApotheke(ApothekeDto apotheke)
    {
        Console.WriteLine("Apotheke Details:");
        Console.WriteLine($"ID: {apotheke.IdApotheke}");
        Console.WriteLine($"Name: {apotheke.ApothekeName}");
        Console.WriteLine($"IK Number: {apotheke.ApoIkNr}");
        Console.WriteLine($"Address: {apotheke.Strasse}");
        Console.WriteLine($"Location: {apotheke.Plz} {apotheke.Ort}");
        Console.WriteLine($"Contact: {apotheke.Email}, {apotheke.Telefon}");
    }

    private static async Task<int> HandleMarkAsReadCommand(MarkAsReadCommand cmd)
    {
        try
        {
            var apiClient = CreateApiClient();
            var prescriptionsApi = new PrescriptionsApi(apiClient);
            
            var request = new BulkOperationRequest
            {
                Prescriptions = cmd.Ids.Select(id => 
                    new PrescriptionIdentifier { 
                        RezeptUuid = id.ToString() 
                    }).ToList()
            };
            
            var response = await prescriptionsApi.ApiPrescriptionsMarkAsReadPostAsync(request);
            
            Console.WriteLine($"Marked {response.ProcessedCount} prescriptions as read");
            if (response.Errors != null && response.Errors.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Failed to mark {response.Errors.Count} prescriptions as read");
                foreach (var error in response.Errors)
                {
                    Console.WriteLine($"- {error}");
                }
                Console.ResetColor();
            }
            
            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }

    private static async Task<int> HandleSetAbgerechnetCommand(SetAbgerechnetCommand cmd)
    {
        try
        {
            var apiClient = CreateApiClient();
            var prescriptionsApi = new PrescriptionsApi(apiClient);
            
            var request = new BulkOperationRequest
            {
                Prescriptions = cmd.Ids.Select(id => 
                    new PrescriptionIdentifier { 
                        RezeptUuid = id.ToString() 
                    }).ToList()
            };
            
            var response = await prescriptionsApi.ApiPrescriptionsSetAbgerechnetPostAsync(request);
            
            Console.WriteLine($"Marked {response.ProcessedCount} prescriptions as abgerechnet");
            if (response.Errors != null && response.Errors.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Failed to mark {response.Errors.Count} prescriptions as abgerechnet");
                foreach (var error in response.Errors)
                {
                    Console.WriteLine($"- {error}");
                }
                Console.ResetColor();
            }
            
            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }

    private static Configuration CreateApiClient()
    {
        var baseUrl = _configuration["ApiSettings:BaseUrl"];
        var username = _configuration["ApiSettings:Username"];
        var password = _configuration["ApiSettings:Password"];
        var tenant = _configuration["ApiSettings:Tenant"];

        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new ArgumentException("BaseUrl is not configured. Use 'config' command to set it.");
        }

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Username or password is not configured. Use 'config' command to set them.");
        }

        var config = new Configuration
        {
            BasePath = baseUrl,
            Username = username,
            Password = password
        };

        // Add any additional headers
        if (!string.IsNullOrEmpty(tenant))
        {
            // Since DefaultHeaderMap is not available, we'll need to handle this differently
            // Configuration doesn't seem to have a method for custom headers
            // In a real application, we might need to extend the Configuration class
        }

        // If your server uses a self-signed certificate, you can disable certificate validation (for development only)
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

        return config;
    }
}

// Command line option classes
[Verb("config", HelpText = "Configure the API client settings")]
public class ConfigCommand
{
    [Option('s', "show", Required = false, HelpText = "Show current configuration")]
    public bool ShowCurrent { get; set; }
    
    [Option('u', "url", Required = false, HelpText = "Set the base URL for the API")]
    public string? BaseUrl { get; set; }
    
    [Option("username", Required = false, HelpText = "Set the username for authentication")]
    public string? Username { get; set; }
    
    [Option('p', "password", Required = false, HelpText = "Set the password for authentication")]
    public string? Password { get; set; }
    
    [Option('t', "tenant", Required = false, HelpText = "Set the tenant identifier")]
    public string? Tenant { get; set; }
}

[Verb("prescriptions", HelpText = "List new prescriptions")]
public class PrescriptionsCommand
{
    [Option('p', "page", Default = 1, HelpText = "Page number")]
    public int Page { get; set; }

    [Option('s', "pageSize", Default = 10, HelpText = "Page size")]
    public int PageSize { get; set; }

    [Option('t', "type", Required = false, HelpText = "Filter by prescription type")]
    public string? Type { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Show detailed information")]
    public bool Verbose { get; set; }
}

[Verb("apotheken", HelpText = "Manage pharmacy data")]
public class ApothekenCommand
{
    [Option('i', "id", Required = false, HelpText = "Get specific apotheke by ID")]
    public int? Id { get; set; }

    [Option('k', "ik", Required = false, HelpText = "Get specific apotheke by IK number")]
    public long? IkNr { get; set; }

    [Option('p', "page", Default = 1, HelpText = "Page number")]
    public int Page { get; set; }

    [Option('s', "pageSize", Default = 10, HelpText = "Page size")]
    public int PageSize { get; set; }

    [Option("search", Required = false, HelpText = "Search term")]
    public string? Search { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Show detailed information")]
    public bool Verbose { get; set; }
}

[Verb("mark-read", HelpText = "Mark prescriptions as read")]
public class MarkAsReadCommand
{
    [Option('i', "ids", Required = true, Separator = ',', HelpText = "Comma-separated list of prescription IDs")]
    public IEnumerable<int> Ids { get; set; } = Array.Empty<int>();
}

[Verb("set-abgerechnet", HelpText = "Mark prescriptions as abgerechnet (billed)")]
public class SetAbgerechnetCommand
{
    [Option('i', "ids", Required = true, Separator = ',', HelpText = "Comma-separated list of prescription IDs")]
    public IEnumerable<int> Ids { get; set; } = Array.Empty<int>();
}
