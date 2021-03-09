using System;
using System.IO;
using System.Text;
namespace BDlab1{
    class Program : Function
    {
        static void Main(string[] args){
            const string namefile = "BD.bin";
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
                        AddOnEnd(namefile, idZ,lastname,name,midlename,idG);
                        break;
                    }   
                    case "2":
                    {
                        
                        Edit();
                        break;
                    }
                    case "3":
                    {
                        Console.Write("Введите номер зачётки студента которого вы хотите удалить: ");
                        int idZ = Convert.ToInt32(Console.ReadLine());
                        Remove(idZ,namefile);
                        break;
                    }
                    case "4":
                    {
                        Console.Write("Введите номер зачётки студента которого вы ищете: ");
                        int idZ = Convert.ToInt32(Console.ReadLine());
                        Search(idZ,namefile);
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}