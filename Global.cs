using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphCLICommandCenter
{
	public static class Global
	{
		public static string UserID = "Test";

		public static string appID { get; set; }

		public static string scopeString { get; set; }

		public static string emails { get; set; }

		public static string authToken { get; set; }

		public static IConfigurationRoot appConfiguration { get; set; }


		/*This was the constructor before Global was turned into a static class
		public Global(string appID, string scopeString, string emails, string authToken)
		{
			this.appID = appID;
			this.scopeString = scopeString;
			this.emails = emails;
			this.authToken = authToken;
		}*/

		static Global()
		{
			var appConfig = LoadAppSettings();
			appConfiguration = appConfig;
			appID = appConfig["appID"];
			scopeString = appConfig["scopes"];
			emails = appConfig["emails"];
			authToken = appConfig["authToken"];
		}

		 static IConfigurationRoot LoadAppSettings()
		{
			var appConfig = new ConfigurationBuilder()
				.AddUserSecrets<Program>()
				.Build();

			// Check for required settings
			if (string.IsNullOrEmpty(appConfig["appId"]) ||
				string.IsNullOrEmpty(appConfig["scopes"]))
			{
				Console.WriteLine("App config is missing permission and scope");
				return null;
			}

			return appConfig;
		}
	}
}
