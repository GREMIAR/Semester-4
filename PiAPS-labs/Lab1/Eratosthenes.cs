using System;
using System.Collections.Generic;
namespace PiAPS_labs{
    class Eratosthenes
    {
        public static void WriteEratosthenes(int upTo)
        {
            if(upTo<2)
            {
                return;
            }
            List<int> numbers = new List<int>();
            numbers.Add(2);
            for(int number=3;number<=upTo;number++)
            {
                if(number%2!=0)
                {
                    numbers.Add(number);
                }
            }
            int index=0;
            int sieve=2;
            while(index+1<numbers.Count)
            {
                sieve = numbers[++index];
                int counter=0;
                while(counter<numbers.Count)
                {
                    if(numbers[counter]%sieve==0&&numbers[counter]!=sieve)
                    {
                        numbers.Remove(numbers[counter]);
                    }
                    counter++;
                }
            }
            foreach (int number in numbers)
            {
                Console.Write(number+"; ");
            }
        }
    }
}