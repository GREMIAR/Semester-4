using System;
namespace PiAPS_labs
{
    class Fibonacci
    {
        public static void WriteFibonacci(ulong upTo)
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
    }
}