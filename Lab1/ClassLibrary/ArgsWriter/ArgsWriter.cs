using System;

namespace ArgsWriter
{
    public class ArgsWriter
    {
        public static void ArgsOutput(string [] args)
        {
            foreach(string arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}
