# Outlook-CLI-Command-Center
.Net Core CLI app that allows users to send outlook emails from the command line. 
Planning on introducing more features from Microsoft Graph APIs


### Prerequisites

Application currently requires you to register the app in azure, you can follow this tutorial on how to do that (https://docs.microsoft.com/en-us/graph/tutorials/aspnet?tutorial-step=2)
You also need to have a secrets.json file if you're developing locally. You can do that by:

```
dotnet user-secrets init
dotnet user-secrets set appId "YOUR_APP_ID_HERE"
dotnet user-secrets set scopes "User.Read;Mail.ReadWrite;Mail.Send"
```
If you have predetermined email addresses you know you'll be emailing often you can also input them as a user-secret separated by semicolons. Note: In the program these addresses will be used if you input "hr" (cap insensitive), you can change that in Program.cs.
```
dotnet user-secrets set emails "john@doe.com;jane@doe.com;place@holder.net"
```

### Usage

When you run the program you'll be prompted to sign in via Microsoft.com/DeviceLogin and will enter the given security code.


```
TODO
```



