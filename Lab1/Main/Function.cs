/*using System;
using System.Collections.Generic;
namespace PiAPS_labs{
    internal class Function
    {
        public static void FindLeapYear(int from,int upTo)
        {
            for(;from<=upTo;from++)
            {
                if(from%4==0)
                {
                    if(from%400!=0&&from%100==0)
                    {
                        Console.WriteLine("Не високосный: "+ from);
                    }
                    else
                    {
                        Console.WriteLine("Високосный: "+ from);
                    }
                }
                else
                {
                    Console.WriteLine("Не високосный: "+ from);
                }
            }
        }
        public static void ArgsWriter(string [] args)
        {
            foreach(string arg in args)
            {
                Console.WriteLine(arg);
            }
        }
        public static void Factorial(ulong number)
        {
            ulong byf;
            for(byf=1;number>0;number--)
            {
                byf*=number;
            }
            Console.WriteLine(byf);
        }
        public static void Fibonacci(ulong upTo)
        {
            if(upTo<1)
            {
                return;
            }
            ulong one=1,two=1,byf;
            Console.WriteLine(one);
            while(two<=upTo)
            {
                Console.WriteLine(two);
                byf=two;
                two+=one;
                one=byf;
            }
        }
        public static void Eratosthenes(int upTo)
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
}*/