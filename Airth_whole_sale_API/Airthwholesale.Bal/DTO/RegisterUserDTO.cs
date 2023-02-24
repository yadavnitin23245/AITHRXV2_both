namespace Airthwholesale.Bal.DTO
{
    public class RegisterUserDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        public string businessname { get; set; }
        public string gstNumber { get; set; }
        public string EFTinfo { get; set; }
        public string paymenttype { get; set; }

        public string addresslineone { get; set; }
        public string addresslinetwo { get; set; }
        public string cityName { get; set; }

        public string Country { get; set; }
        public string State { get; set; }

        public List<AccountUserDTO> userlist { get; set; }


    }
}
