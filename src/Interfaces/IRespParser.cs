namespace codecrafters_redis.src.Interfaces
{
    public interface IRespParser
    {
        public Endpoint ParseRespString(byte[] buffer, int readTotal);
    }
}
