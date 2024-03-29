﻿using System;
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
            Console.WriteLine("\n\n\t\t\t\t\t\tBy @jc4st3lls");
            Console.ReadLine();

        }

        private static void Test1()
        {
            const string SECRET= "Hello Shamir Secret Share";
            const int NUMPARTS = 5;
            const int MINIMUMPARTS = 3;

            Console.WriteLine($"Secret: {SECRET}");

            var shamirss = new Crypto.ShamirSS(new Crypto.SecureRandom(), NUMPARTS, MINIMUMPARTS);
            var secret = Encoding.UTF8.GetBytes(SECRET);
            ImmutableDictionary<int, byte[]> parts = shamirss.Split(secret);

            foreach (var part in parts)
            {
                // Print parts b64 and Hex formats
                Console.WriteLine($"Part {part.Key}: b64 [{Convert.ToBase64String(part.Value)}] " +
                    $"Hex:[{BitConverter.ToString(part.Value).Replace("-",string.Empty)}]");
            }
            // Reccover with all parts
            var recovered = shamirss.Join(parts);
            Console.WriteLine($"Recover with all parts -> {Encoding.UTF8.GetString(recovered)}");
            // Recover with minum parts 
            var recoveredMinium = shamirss.Join(parts.Take(MINIMUMPARTS).ToDictionary(t=>t.Key,v=>v.Value));
            Console.WriteLine($"Recover with minium parts -> {Encoding.UTF8.GetString(recoveredMinium)}");
        }
    }
}
