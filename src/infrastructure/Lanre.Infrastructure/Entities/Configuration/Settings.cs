namespace Lanre.Infrastructure.Entities.Configuration
{
    public class Settings
    {
        public ConnetionStrings ConnectionStrings { get; set; }
        public HttpsConfig HttpsConfig { get; set; }
    }

    public class ConnetionStrings
    {
        public string Scheduler { get; set; }
    }

    public class HttpsConfig
    {
        public int Port { get; set; }
    }
}
