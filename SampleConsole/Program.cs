using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();


            Console.WriteLine("Press any key!!");
            Console.ReadLine();

        }

        private static void Test1()
        {
            const string SECRET= "Hello Shamir Secret Share";
            const int NUMPARTS = 5;
            const int MINIUMPARTS = 3;

            Console.WriteLine($"Secret: {SECRET}");

            var shamirss = new Crypto.ShamirSS(new Crypto.SecureRandom(), NUMPARTS, MINIUMPARTS);
            var secret = Encoding.UTF8.GetBytes(SECRET);
            ImmutableDictionary<int, byte[]> parts = shamirss.Split(secret);

            foreach (var part in parts)
            {
                Console.WriteLine($"Part {part.Key}: [{Convert.ToBase64String(part.Value)}]");
            }
            var recovered = shamirss.Join(parts);
            Console.WriteLine($"Recover with all parts -> {Encoding.UTF8.GetString(recovered)}");
            var recovered3 = shamirss.Join(parts.Take(MINIUMPARTS).ToDictionary(t=>t.Key,v=>v.Value));
            Console.WriteLine($"Recover with minium parts -> {Encoding.UTF8.GetString(recovered3)}");
        }
    }
}
