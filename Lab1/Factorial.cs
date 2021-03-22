using System;
namespace PiAPS_labs
{
    class Factorial
    {
        public static void FindFactorial(ulong number)
        {
            ulong byf;
            for(byf=1;number>0;number--)
            {
                byf*=number;
            }
            Console.WriteLine(byf);
        }
    }
}