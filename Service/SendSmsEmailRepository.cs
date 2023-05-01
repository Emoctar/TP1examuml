using System.Net.Mail;
using System.Net;
using TP1examuml.Interface;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Vonage;
using Vonage.Request;

namespace TP1examuml.Service
{
    public class SendSmsEmailRepository : ISendSmsEmailRepository
    {
        public async Task<string> SendEmailAsync(string email, string subject, string body)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("moctardiallo1916@gmail.com",
                    "Redefere72");
                smtpClient.EnableSsl = true;

                //Create Mail Message
                using (var message = new MailMessage("moctardiallo1916@gmail.com", email))
                {
                    message.Subject = subject;
                    message.Body = body;

                    try
                    {
                        //Send Email
                        smtpClient.Send(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return "Ok";
        }

   


        public async Task<string> SendSmsAsync(string telephone, string body)
        {
            string accountSid = "ACaab80dca1a0c4fb345277f7a62e926a6";
            // Your Auth Token from twilio.com/console
            string authToken = "c1134c356d75d86eb40bf69483dd2cf2";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber("+221781397254"),
                to: new Twilio.Types.PhoneNumber("+221765001787")
            );
            return "ok";
        }

        //Task<string> ISendSmsEmailRepository.SendSmsAsync(string telephone)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<string> ISendSmsEmailRepository.SendEmailAsync(string email, string subject, string body)
        //{

        //    throw new NotImplementedException();
        //}

        public async Task<string> SendSmsAsync(string body)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
    "beef8cb1",
    "rlGAZHXoE2O3fivE"
         );

            var VonageClient = new VonageClient(credentials);

            var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
            {
                To = "221781397254",
                From = "Repository",
                Text =body
            });

            return "ok";
        }

    }

}