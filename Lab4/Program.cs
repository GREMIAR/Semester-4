using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var mat=new Matrix();
            string input="";
            while (input!="9"){
                Console.Write("Ввод: ");
                input=Console.ReadLine();
                switch (input)
                {
                    case "1":
                    {
                        int size;
                        Console.Write("Ввод: ");
                        size = Convert.ToInt32(Console.ReadLine());
                        mat = new Matrix(size);
                        break;
                    }   
                    case "2":
                    {
                        break;
                    }
                    case "3":
                    {
                        break;
                    }
                    case "4":
                    {
                        break;
                    }
                    case "5":
                    {
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}
