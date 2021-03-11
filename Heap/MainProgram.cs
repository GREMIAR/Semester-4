using System;
using System.IO;
namespace BDlab1{
    class Program : Function
    {
        static void Main(string[] args){
            const string filename = "BD.bin";
            BinaryWriter writer;
            try{
                writer = new BinaryWriter(File.Open(filename, FileMode.Open));
            }
            catch (System.IO.FileNotFoundException)
            {
                using (writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(1);
                }  
            }
            writer.Close();
            OurBlock mainBlock = new OurBlock();
            string a="";
            while (a!="9"){
                Console.Write("1-Добавление информации о студент\n2-Изменение информации о студенте\n3-Удаление информации о студенте\n4-Осуществление поиска информации о студенте\nВвод: ");
                a=Console.ReadLine();
                switch (a)
                {
                    case "1":
                    {
                        Console.Write("Номер зачётки: ");
                        int idZ = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Фамилия: ");
                        string lastname = Console.ReadLine();
                        Console.Write("Имя: ");
                        string name = Console.ReadLine();
                        Console.Write("Отчество: ");
                        string midlename = Console.ReadLine();
                        Console.Write("Номер группа: ");
                        int idG = Convert.ToInt32(Console.ReadLine());
                        mainBlock.AddOnEnd(filename, idZ,lastname,name,midlename,idG);
                        break;
                    }   
                    case "2":
                    {
                        Console.Write("Введите номер зачётки сткденкта которого хотиие изменить: ");
                        int oldidz = Convert.ToInt32(Console.ReadLine());
                        Console.Write("На что засменяем: ");
                        Console.Write("Номер зачётки: ");
                        int idZ = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Фамилия: ");
                        string lastname = Console.ReadLine();
                        Console.Write("Имя: ");
                        string name = Console.ReadLine();
                        Console.Write("Отчество: ");
                        string middlename = Console.ReadLine();
                        Console.Write("Номер группа: ");
                        int idG = Convert.ToInt32(Console.ReadLine());
                        mainBlock.Edit(filename,oldidz,idZ,lastname,name,middlename,idG);
                        break;
                    }
                    case "3":
                    {
                        Console.Write("Введите номер зачётки студента которого вы хотите удалить: ");
                        int idZ = Convert.ToInt32(Console.ReadLine());
                        mainBlock.Remove(idZ,filename);
                        break;
                    }
                    case "4":
                    {
                        Console.Write("Введите номер зачётки студента которого вы ищете: ");
                        int idZ = Convert.ToInt32(Console.ReadLine());
                        mainBlock.Search(idZ,filename);
                        break;
                    }
                    default:
                        break;
                } 
            }
        }
    }
}