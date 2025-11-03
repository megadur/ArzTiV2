# ArzTiClient.Con - CLI for ArzTi3Server

This console application provides a command-line interface to interact with the ArzTi3Server using the ArzTiClient library.

## Configuration

Before using the application, you need to set up the API connection details in the `appsettings.json` file:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://your-server-url",
    "Username": "your-username",
    "Password": "your-password",
    "Tenant": "your-tenant-id"
  }
}
```

You can view or update the configuration using the CLI:

```
# Show current configuration
ArzTiClient.Con config --show

# Update the base URL
ArzTiClient.Con config -u https://new-server-url

# Update credentials
ArzTiClient.Con config --username new-user -p new-password

# Update tenant
ArzTiClient.Con config -t new-tenant
```

## Usage

The CLI provides several commands to interact with the API:

### Working with Prescriptions

List new prescriptions:
```
ArzTiClient.Con prescriptions
```

Options:
- `-p, --page <number>`: Page number (default: 1)
- `-s, --pageSize <number>`: Page size (default: 10)
- `-t, --type <type>`: Filter by prescription type
- `-v, --verbose`: Show detailed information

Mark prescriptions as read:
```
ArzTiClient.Con mark-read -i 123,456,789
```

Mark prescriptions as billed (abgerechnet):
```
ArzTiClient.Con set-abgerechnet -i 123,456,789
```

### Working with Pharmacies (Apotheken)

List all pharmacies:
```
ArzTiClient.Con apotheken
```

Options:
- `-p, --page <number>`: Page number (default: 1)
- `-s, --pageSize <number>`: Page size (default: 10)
- `--search <term>`: Search term
- `-v, --verbose`: Show detailed information

Get pharmacy by ID:
```
ArzTiClient.Con apotheken -i 123
```

Get pharmacy by IK number:
```
ArzTiClient.Con apotheken -k 123456789
```

## Error Handling

The application will display error messages in red. Common errors include:

- Connection issues: Check your BaseUrl in appsettings.json
- Authentication issues: Verify your username and password
- Tenant issues: Make sure the tenant ID is correct

## Development

The application uses:
- .NET 8.0
- CommandLineParser for parsing command-line arguments
- ArzTiClient library for API communication
- Newtonsoft.Json for JSON configuration handling