using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AirthwholesaleAPI.Email
{
    public class EmailHelper
    {
        private readonly IOptions<AppSettingsDTO> _appSettings;
        protected IICCBatchLogic _IICCBatchLogicBAL { get; private set; }
        public EmailHelper(IOptions<AppSettingsDTO> appSettings, IICCBatchLogic iICCBatchLogicBAL)
        {
            _appSettings = appSettings;
            _IICCBatchLogicBAL = iICCBatchLogicBAL;
        }


        public bool SendCBBEmail(string userEmail, string confirmationLink)
        {

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.AppSettings.ToString();
            string KeyCategory = ZStoreKeyCategory.SMTP.ToString();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.SMTP.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);


            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(APIValuesList.EmailAccountValue);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "CBB API Details";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(APIValuesList.SMTPUserNameValue, APIValuesList.SMTPPasswordValue);
            client.Host = APIValuesList.SMTPServerNameValue;
            client.Port = Convert.ToInt32(APIValuesList.SMTPPortValue);
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (AppException ex)
            {
                // log exception
            }
            return false;
        }

        /// <summary>
        ////This funtion is responsible for shoot the mail
        /// </summary>
        /// <returns></returns>
        ///
        public bool SendEmail(string userEmail, string confirmationLink)
        {

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.AppSettings.ToString();
            string KeyCategory = ZStoreKeyCategory.SMTP.ToString();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.SMTP.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);


            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(APIValuesList.EmailAccountValue);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(APIValuesList.SMTPUserNameValue, APIValuesList.SMTPPasswordValue);
            client.Host = APIValuesList.SMTPServerNameValue;
            client.Port =Convert.ToInt32 (APIValuesList.SMTPPortValue);
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (AppException ex)
            {
                // log exception
            }
            return false;
        }

        public bool SendEmailPasswordReset(string userEmail, string link)
        {
         
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.AppSettings.ToString();
            string KeyCategory = ZStoreKeyCategory.SMTP.ToString();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.SMTP.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(APIValuesList.EmailAccountValue);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;
          
            using (SmtpClient smtpClient = new SmtpClient(APIValuesList.SMTPServerNameValue, Convert.ToInt32(APIValuesList.SMTPPortValue)))
            {
                
                smtpClient.Credentials = new NetworkCredential(APIValuesList.SMTPUserNameValue, APIValuesList.SMTPPasswordValue);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
          
                try
                {
                    smtpClient.Send(mailMessage);
                    return true;
                }
                catch (AppException ex)
                {
                    // log exception
                }
            }
            return false;
        }

        public bool SendEmailTwoFactorCode(string userEmail, string code)
        {
            
            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string StoreName = ZStoreName.AppSettings.ToString();
            string KeyCategory = ZStoreKeyCategory.SMTP.ToString();

            // Getting Values StoreName and KeyCategory  form ENUM For Common Values
            string KeyCategoryICCBatchValues = ZStoreKeyCategory.SMTP.ToString();

            // Function for getting API values 
            var APIValuesList = _IICCBatchLogicBAL.GetJDPAPIKeyValues(StoreName, KeyCategory, KeyCategoryICCBatchValues);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(APIValuesList.EmailAccountValue);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Two factor code";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = code;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(APIValuesList.SMTPUserNameValue, APIValuesList.SMTPPasswordValue);
            client.Host = APIValuesList.SMTPServerNameValue;
            client.Port = Convert.ToInt32(APIValuesList.SMTPPortValue);
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (AppException ex)
            {
                // log exception
            }
            return false;
        }
    }
}
