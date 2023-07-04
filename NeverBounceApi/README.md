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