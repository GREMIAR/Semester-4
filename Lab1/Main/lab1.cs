using System;
using ArgsWriter;
namespace PiAPS_labs
{
    class Program
    {
        static void Main(string[] args)
        {
            string input="";
            while (input!="9"){
                Console.Write("\n1. Вывод на экран аргументов, переданные в программу при запуске в командной строке. \n2. Вывод високосных лет с 1900 по 2000 гг\n3. Вывод последовательность чисел Фибоначи до заданного числа.\n4. Вычисление факториала заданного числа.\n5. Вывод всеч простых чисел не превышающие заданное.\nВвод: ");
                input=Console.ReadLine();
                switch (input)
                {
                    case "1":
                    {
                        ArgsWriter.ArgsOutput(args);
                        //ArgsWriter.ArgsWriter1(args);
                        break;
                    }   
                    /*case "2":
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
                    }*/
                    default:
                        break;
                } 
            }
        }
    }
}
