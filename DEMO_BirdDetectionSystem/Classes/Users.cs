namespace BirdDetectionSystem
{
    class Users
    {
        private string userName;
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }

        public Users()
        {

        }

        public Users(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
    }
}
