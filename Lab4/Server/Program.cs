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
                        Console.Write("Ввод размерв: ");
                        size = Convert.ToInt32(Console.ReadLine());
                        mat = new Matrix(size);
                        break;
                    }   
                    case "2":
                    {
                        for(int i=0;i<mat.Size;i++)
                        {
                            for(int f=0;f<mat.Size;f++)
                            {
                                mat.SetCell(i,f,i*mat.Size+f+2);
                            }
                        }
                        mat.SetCell(0,1,1);
                        break;
                    }
                    case "3":
                    {
                        for(int i=0;i<mat.Size;i++)
                        {
                            for(int f=0;f<mat.Size;f++)
                            {
                                Console.Write(mat.Cell(i,f));
                            }
                            Console.WriteLine();
                        }
                        break;
                    }
                    case "4":
                    {
                        mat.Reset();
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}
