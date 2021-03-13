using System;

namespace PiAPS_labs
{
    class Program : Function
    {
        static void Main(string[] args)
        {
            string input="";
            while (input!="9"){
                Console.Write("\n1-Добавление информации о студент\n2-Изменение информации о студенте\n3-Удаление информации о студенте\n4-Осуществление поиска информации о студенте\nВвод: ");
                input=Console.ReadLine();
                switch (input)
                {
                    case "1":
                    {
                        ArgsWriter(args);
                        break;
                    }   
                    case "2":
                    {
                        FindLeapYear(1900,2000);
                        break;
                    }
                    case "3":
                    {
                        input=Console.ReadLine();
                        Fibonacci(ulong.Parse(input));
                        break;
                    }
                    case "4":
                    {
                        input=Console.ReadLine();
                        Factorial(ulong.Parse(input));
                        break;
                    }
                    case "5":
                    {
                        input=Console.ReadLine();
                        Eratosthenes(int.Parse(input));
                        break;
                    }
                    default:
                        break;
                } 
            }
        }
    }
}
