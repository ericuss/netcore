namespace Lanre.Infrastructure.Entities.Configuration
{
    public class Settings
    {
        public HttpsConfig HttpsConfig { get; set; }
    }

    public class HttpsConfig
    {
        public int Port { get; set; }
    }
}
