namespace codecrafters_redis.src.Interfaces
{
    public class Endpoint
    {
        public string EndpointName { get; set; }
        public string? Value { get; set; }
        private static readonly List<string> AllowedEndpointNames = new List<string> { "ECHO", "PING" };

        private Endpoint(string endpointName, string? value)
        {
            Value = value;
            EndpointName = endpointName;
        }
        public static Endpoint CreateEndpoint(string endpoint, string? value)
        {
            if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException("Values cannot be null");

            if (AllowedEndpointNames.Contains(endpoint))
            {
                return new Endpoint(endpoint, value);
            }

            throw new Exception("Method not allowed");
        }
    }
}