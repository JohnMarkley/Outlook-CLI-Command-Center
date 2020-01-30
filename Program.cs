using System;
using Microsoft.Extensions.Configuration;


namespace GraphCLICommandCenter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Microsoft Command Center\n");
            
            if (Global.appConfiguration == null)
            {
                Console.WriteLine("Missing or invalid appsettings.json");
                return;
            }

            var scopes = Global.scopeString.Split(';');
          
            // Initialize the auth provider with values from appsettings.json
            var authProvider = new DeviceCodeAuthProvider(Global.appID, scopes);

            // Get access token if none is provided in local env variable
            var accessToken = authProvider.GetAccessToken().Result;


            // Initialize Graph client
            GraphHelper.Initialize(authProvider);

            // Get signed in user
            var user = GraphHelper.GetMeAsync().Result;
            Console.WriteLine($"Welcome {user.DisplayName}!\n");

            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("Please choose one of the following options:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Display access token");
                Console.WriteLine("2. Send Email");

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    // Set to invalid value
                    choice = -1;
                }

                switch (choice)
                {
                    case 0:
                        // Exit the program
                        Console.WriteLine("Goodbye...");
                        break;
                    case 1:
                        // Display access token
                        Console.WriteLine($"Access token: {accessToken}\n");
                        break;
                    case 2:
                        //Test email to jwmarkle@asu.edu
                        TestEmailAsync();
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }


        static async System.Threading.Tasks.Task TestEmailAsync()
        {
            //Method called 
            Console.WriteLine("About to send an email");

            bool sendMail = true;
            while (sendMail)
            {
                var user = GraphHelper.GetMeAsync().Result;
                string mailAddress = user.UserPrincipalName;

                Console.WriteLine("Sending message from " + mailAddress);
                Console.WriteLine("Input recipients (or 'HR'):");

                string userInputAddress = Console.ReadLine();
                Console.WriteLine("Enter first name");
                string userInputFirstName = Console.ReadLine();
                Console.WriteLine("Enter last name");
                string userInputLastName = Console.ReadLine();
                string messageAddress = userInputAddress.Equals("hr", StringComparison.InvariantCultureIgnoreCase) ? Global.emails : userInputAddress;
                
                await GraphHelper.ComposeAndSendMailAsync(userInputFirstName + "'s AD Account", userInputFirstName, userInputLastName, messageAddress);

                sendMail = false;

            }

           




        }
    }

}