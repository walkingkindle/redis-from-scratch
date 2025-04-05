namespace codecrafters_redis.src.Interfaces
{
    public interface IEndpointHandler
    {
        public string Handle(Endpoint endpoint);
    }
}
