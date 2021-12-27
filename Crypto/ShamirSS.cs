using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Crypto
{
    public class ShamirSS
    {
        private readonly SecureRandom _random;
        public int N { get; private set; }
        public int K { get; private set; }
        public ShamirSS(SecureRandom secureRandom, int n, int k)
        {
            if (!(k > 1)) throw new ArgumentException("k > 1");
            if (!(n >= k)) throw new ArgumentException("n >= k");
            if (!(n <= 255)) throw new ArgumentException("n <= 255");
            _random = secureRandom;
            N = n;
            K = k;

        }
        public ImmutableDictionary<int, byte[]> Split(byte[] secret)
        {

            byte[,] values = new byte[N, secret.Length];

            for (int i = 0; i < secret.Length; i++)
            {
                // for each byte, generate a random polynomial, p
                byte[] p = GF256.Generate(_random, K - 1, secret[i]);
                for (int x = 1; x <= N; x++)
                {
                    // each part's byte is p(partId)
                    values[x - 1,i] = GF256.Eval(p, (byte)x);
                }
            }

            // return as a set of objects
            IDictionary<int, byte[]> parts = new Dictionary<int, byte[]>(N);
            for (int i = 0; i < N ; i++)
            {
                byte[] _tmp = new byte[secret.Length];
                for (int ii = 0; ii < secret.Length; ii++)
                {
                    _tmp[ii] = values[i, ii];
                }
                parts.Add(i + 1, _tmp);
            }
            return ImmutableDictionary.ToImmutableDictionary(parts);
           
        }

        public byte[] Join(IDictionary<int, byte[]> parts)
        {
            if (!(parts.Count > 0)) throw new ArgumentException( "No parts provided");
            int[] lengths = parts.Select(s => s.Value.Length).Distinct().ToArray(); 

            if (!(lengths.Length == 1)) throw new Exception("Varying lengths of part values");
            byte[] secret = new byte[lengths[0]];
            for (int i = 0; i < secret.Length; i++)
            {
                byte[,] points = new byte[parts.Count,2];
                int j = 0;

                var enu = parts.GetEnumerator();
                while (enu.MoveNext())
                {
                    points[j, 0] = Convert.ToByte(enu.Current.Key);
                    points[j, 1] = enu.Current.Value[i];
                    j++;
                }

                
                secret[i] = GF256.Interpolate(points);
            }
            return secret;
        }
    }
}
