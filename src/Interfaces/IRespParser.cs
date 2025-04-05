namespace codecrafters_redis.src.Interfaces
{
    public interface IRespParser
    {
        public string ParseRespString(byte[] buffer, int readTotal);
    }
}
