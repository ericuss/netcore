namespace Lanre.Clients.Host.Middelwares
{
    using Newtonsoft.Json;

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ExtendedMessage { get; set; }
        public string StackTrace { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}