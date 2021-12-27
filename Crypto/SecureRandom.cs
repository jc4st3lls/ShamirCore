using System;

namespace Crypto
{
    public class SecureRandom
    {
        private readonly Random _random;
        public SecureRandom()
        {
            _random = new Random(DateTime.Now.Millisecond);
            
        }

        public int Next()
        {
            return GetRandom(_random.Next()).Next();
        }
        public int Next(int minValue, int maxValue)
        {
            return GetRandom(_random.Next()).Next(minValue, maxValue);
        }
        public void NextBytes(byte[] buffer)
        {
            GetRandom(_random.Next()).NextBytes(buffer);
        }
        public int Next(int maxValue)
        {
            return GetRandom(_random.Next()).Next(maxValue);
        }
        public double NextDouble()
        {
            return GetRandom(_random.Next()).NextDouble();
        }

        private Random GetRandom(int seed)
        {
            return new Random(seed + DateTime.Now.Millisecond);
        }
    }
}
