using System;
namespace PiAPS_labs
{
    public class Args
    {
        public static void ArgsWriter(string [] args)
        {
            foreach(string arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}