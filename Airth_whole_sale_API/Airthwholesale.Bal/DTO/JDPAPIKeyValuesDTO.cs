namespace Airthwholesale.Bal.DTO
{
    public class JDPAPIKeyValuesDTO
    {

        public string APIBaseURL { get; set; }

        public string ApikeyValue { get; set; }

        public string DealersValue { get; set; }

        public string PagenoValue { get; set; }

        public string formatValue { get; set; }

        public string ApiAddOnUrl { get; set; }

        public string startDateValue { get; set; }

        public string UpdatesFromValue { get; set; }


        // For Carfex API
        public string ClientID { get; set; }

        public string Clientsecret { get; set; }
        public string audienceValue { get; set; }
        public string granttypeValue { get; set; }

        // for AppSettings values


        public string SMTPServerNameValue { get; set; }
        public string SMTPUserNameValue { get; set; }
        public string SMTPPasswordValue { get; set; }
        public string SMTPPortValue { get; set; }
        public string EmailAccountValue { get; set; }
        public string EnableSslValue { get; set; }
        public string ResetpasswordURLValue { get; set; }

        

        public string RefreshTokenTTLValue { get; set; }

        public string JWT_SecretValue { get; set; }

        
        // For for JWT
        public string KeyValue { get; set; }
        public string IssuerValue { get; set; }
        public string AudienceValue { get; set; }
        public string SubjectValue { get; set; }
        public string ValidAudienceValue { get; set; }

        public string ValidIssuerValue { get; set; }
        public string SecretValue { get; set; }


        // for CBB API VALUES

        public string country { get; set; }
        public string customerid { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string URL { get; set; }

    }
}
