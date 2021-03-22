using System;
namespace PiAPS_labs
{
    class LeapYear
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
    }
}