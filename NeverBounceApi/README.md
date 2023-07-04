﻿# NeverBounce CLI

Example project for using NeverBounce in .NET, a command-line interface implementation.

## Usage
- `neverbounce.cli account` get account info.
- `neverbounce.cli single <email>` check single email.
- `neverbounce.cli jobs` list jobs.
  - `neverbounce.cli jobs --page:2` show page 2 of the jobs.
- `neverbounce.cli jobs create <file>` upload a file as a new batch job.
  - If the file is online this will point to the file - NeverBounce.com needs to be able to access the location.
  - If the file is local it will parse and be uploaded as an array.
- `neverbounce.cli jobs download <job ID>` download the results of a job.
  - `neverbounce.cli jobs download <job ID> --file:<output file>` optionally populate a file with the result.
- `neverbounce.cli jobs parse <job ID>` parse a bulk job.
- `neverbounce.cli jobs start <job ID>` start a bulk job - this will use credits.
  - `neverbounce.cli jobs start <job ID> --run-sample` run a sample for a bulk job.
- `neverbounce.cli jobs status <job ID>` get the current status of a job.
- `neverbounce.cli jobs result <job ID>` get the results of a job.
  - `neverbounce.cli jobs result <job ID> --page:2` show page 2 of the results.
- `neverbounce.cli jobs delete <job ID>` delete a job.

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
