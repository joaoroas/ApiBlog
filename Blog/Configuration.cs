namespace Blog
{
    public static class Configuration
    {
        public static string JwtKey { get; set; } = "ef912761-c9d1-49de-b8d9-e193ded8d917";
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "curso_api_44f1b13b781bd604b74bed20eb2d573b";
        public static SmtpConfiguration Smtp = new SmtpConfiguration();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
