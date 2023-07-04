# NeverBounce CLI

Example project for using NeverBounce in .NET, a command-line interface implementation.

## Usage
- `neverbounce.cli account` get account info.
- `neverbounce.cli single <email>` check single email.
- `neverbounce.cli jobs` list jobs.
  - `neverbounce.cli jobs --page:2` show page 2 of the jobs.
- `neverbounce.cli jobs create <file>` upload a file as a new batch job
  - If the file is online this will point to the file - NeverBounce.com needs to be able to access the location    
  - If the file is local it will parse and be uploaded as an array

## Config
Configuration is held in an `appsettings.json` file:

```json
{
  "NeverBounce": {
    // Your API key can be found in your dashboard at https://app.neverbounce.com/apps, set it in your secrets file
    "key": "secret_████████████████████████",

    // The following are optional, and will default to the values shown below
    //"version": "v4.2",
    //"host": "https://api.neverbounce.com/"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning" // Change to "Information" to see detailed requests
    }
  }
}
```

This project is configured to use a secrets file, by default: `C:\Users\<user>\AppData\Roaming\Microsoft\UserSecrets\neverbounce\secrets.json` - if you fork or branch this code use the secrets file to keep your API key out of source control.
