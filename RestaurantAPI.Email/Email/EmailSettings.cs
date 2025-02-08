namespace RestaurantAPI.Email.Email
{
    public class EmailSettings
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
    }
}
