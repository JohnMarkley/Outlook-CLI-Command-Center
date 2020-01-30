using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GraphCLICommandCenter
{
    public class GraphHelper
    {
        private static GraphServiceClient graphClient;
        public static void Initialize(IAuthenticationProvider authProvider)
        {
            graphClient = new GraphServiceClient(authProvider);
        }

        public static async Task<User> GetMeAsync()
        {
            try
            {
                // GET /me
                return await graphClient.Me.Request().GetAsync();
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }


        public static async Task ComposeAndSendMailAsync(string subject, string firstNameOfEmployee, string lastNameOfEmployee, string recipients)
        {
           
            List<Recipient> recipientList = new List<Recipient>();
            string[] splitter = { ";" };
            var splitRecipientsString = recipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            foreach (string recipient in splitRecipientsString)
            {
                recipientList.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient.Trim() } });
            }
            
           
            try
            {
                var email = new Message
                {
                    Body = new ItemBody
                    {
                        //TODO: Replace hardcoded content
                        Content = "Hey All, \n\n" + firstNameOfEmployee + "'s AD account has been created and is reachable at " + firstNameOfEmployee + "." + lastNameOfEmployee + Global.companyName +"\n\nThanks, \nJohn Markley\nIT Support",
                        
                        ContentType = BodyType.Text,
                    },
                    Subject = subject,
                    ToRecipients = recipientList
                };

                try
                {
                    await graphClient.Me.SendMail(email, true).Request().PostAsync();
                }
                catch (ServiceException exception)
                {
                    throw new Exception("Message did not send: " + exception.Error == null ? "No error message returned." : exception.Error.Message);
                }

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong: " + e.Message);
            }
        }




    }
}