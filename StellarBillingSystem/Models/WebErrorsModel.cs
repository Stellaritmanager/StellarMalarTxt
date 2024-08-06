namespace StellarBillingSystem.Models
{
    public class WebErrorsModel
    {
        private string errodDesc;
        private string screenName;
        private string username;
        private string DateTime;
        private string machineName;

        public string ErrodDesc { get => errodDesc; set => errodDesc = value; }
        public string ScreenName { get => screenName; set => screenName = value; }
        public string Username { get => username; set => username = value; }
        public string ErrDateTime { get => DateTime; set => DateTime = value; }
        public string MachineName { get => machineName; set => machineName = value; }
    }
}
