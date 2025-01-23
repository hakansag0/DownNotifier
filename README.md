# DownNotifier

* In Debug Mode, application logs to console if app is not healthy.
* In Release Mode, application uses SMTP protocol to send Email. 
* **"SMTP" environment value MUST NOT BE EMPTY IN RELEASE MODE.**

## Environment Fields

`appsettings.json`
```
{
  ...
  "ConnectionStrings": {
    "MsSqlServer": "{MSSQL_CONNECTION_STRING}"
  },
  //Must not be empty on Release Mode!
  "SMTP": {
    "Host": "{SMTP_HOST}",
    "Port": {SMTP_PORT},
    "EmailAddress": "{SMTP_EmailAddress}",
    "Password": "{SMTP_Password}"
  },
  ...
}
```
