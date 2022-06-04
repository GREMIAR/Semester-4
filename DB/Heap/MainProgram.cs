using System;
using System.IO;
namespace BDlab1{
    class Program
    {
        static void Main(string[] args){
            const string filename = "Heap.bin";
            FileInfo fileSize = new FileInfo(filename);
            BinaryWriter writer;
            try{
                writer = new BinaryWriter(File.Open(filename, FileMode.Open));
            }
            catch (System.IO.FileNotFoundException)
            {
                using (writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(0);
                }  
            }
            writer.Close();
            OurBlock mainBlock = new OurBlock();
            string a="";
            while (a!="9"){
                fileSize.Refresh();
                Console.WriteLine("Размер файла = "+fileSize.Length);
                Console.Write("\n1-Добавление информации о студент\n2-Изменение информации о студенте\n3-Удаление информации о студенте\n4-Осуществление поиска информации о студенте\nВвод: ");
                a=Console.ReadLine();
                try
                {   
                    switch (a)
                    {
                        case "1":
                        {
                            Console.Write("Номер зачётки: ");
                            int idZ = Convert.ToInt32(Console.ReadLine());
                            if(idZ<=0){
                                Console.WriteLine("Нельзя присвоить этот номер зачётки");
                                break;
                            }
                            if((mainBlock.Search(idZ,filename))!=-1){
                                Console.WriteLine("Номер зачётки {0} занят",idZ);
                                break;
                            }
                            
                            Console.Write("Фамилия: ");
                            string lastname = Console.ReadLine();
                            Console.Write("Имя: ");
                            string name = Console.ReadLine();
                            Console.Write("Отчество: ");
                            string middlename = Console.ReadLine();
                            Console.Write("Номер группа: ");
                            int idG = Convert.ToInt32(Console.ReadLine());
                            mainBlock.AddOnEnd(filename, idZ,lastname,name,middlename,idG);
                            break;
                        }   
                        case "2":
                        {
                            Console.Write("Введите номер зачётки студента которого хотите изменить: ");
                            int oldidz = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("На что заменяем: ");
                            Console.Write("Номер зачётки: ");
                            int idZ = Convert.ToInt32(Console.ReadLine());
                            if(idZ==0){
                                Console.WriteLine("Нельзя присвоить этот номер зачётки");
                                break;
                            }
                            int size;
                            if((size=mainBlock.Search(idZ,oldidz,filename))==-1){
                                Console.WriteLine("");
                                break;
                            }
                            Console.Write("Фамилия: ");
                            string lastname = Console.ReadLine();
                            Console.Write("Имя: ");
                            string name = Console.ReadLine();
                            Console.Write("Отчество: ");
                            string middlename = Console.ReadLine();
                            Console.Write("Номер группа: ");
                            int idG = Convert.ToInt32(Console.ReadLine());
                            mainBlock.Edit(filename,size,oldidz, idZ,lastname,name,middlename,idG);
                            break;
                        }
                        case "3":
                        {
                            Console.Write("Введите номер зачётки студента которого вы хотите удалить: ");
                            int idZ = Convert.ToInt32(Console.ReadLine());
                            if(idZ==0){
                                Console.WriteLine("Такого номера нет");
                                break;
                            }
                            mainBlock.Remove(idZ,filename);
                            break;
                        }
                        case "4":
                        {
                            Console.Write("Введите номер зачётки студента которого вы ищете: ");
                            int idZ = Convert.ToInt32(Console.ReadLine());
                            if((idZ=mainBlock.Search(idZ,filename))!=-1){
                                mainBlock.PrintBlock();
                                mainBlock.PrintFindStudent(idZ%5);
                            }
                            else{
                                Console.WriteLine("\nУпс, ничего не удалось найти");
                            }
                            break;
                        }
                        default:
                            break;
                    } 
                }
                catch(System.OverflowException)
                {
                    Console.WriteLine("Вы ввели число за рамками возможного");
                }
            }
        }
    }
}
