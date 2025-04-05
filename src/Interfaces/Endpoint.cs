namespace codecrafters_redis.src.Interfaces
{
    public class Endpoint
    {
        public string EndpointName { get; set; }

        public string? Key { get; set; }
        public string? Value { get; set; }
        private static readonly List<string> AllowedEndpointNames = new List<string> { "ECHO", "PING","GET","SET" };

        private Endpoint(string endpointName, string? value, string key=null)
        {
            Value = value;
            EndpointName = endpointName;
            Key = key;
        }
        public static Endpoint CreateEndpoint(string endpoint, string? value, string key = null)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException("Values cannot be null");

            if (AllowedEndpointNames.Contains(endpoint))
            {
                return new Endpoint(endpoint, value ?? null, key);
            }

            throw new Exception("Method not allowed");
        }
    }
}