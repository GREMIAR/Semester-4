using System;
using System.IO;
namespace Hashed{
    class Program
    {
        static void Main(string[] args){
            const string filename = "Hashed.bin";
            FileInfo fileSize = new FileInfo(filename);
            BinaryWriter writer;
            OurBlock mainBlock = new OurBlock();
            try{
                writer = new BinaryWriter(File.Open(filename, FileMode.Open));
            }
            catch (System.IO.FileNotFoundException)
            {
                using (writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    byte[] nullBlock = new byte[36];
                    writer.Write(nullBlock);
                }
            }
            writer.Close();
            /*for(int i=1;i<50;i++)
            {
                mainBlock.AddOnEnd(filename,i,i.ToString(),i.ToString(),i.ToString(),i);
            }*/
            /*for(int i=1;i<4;i++)
            {
                mainBlock.AddOnEnd(filename,i,i.ToString(),i.ToString(),i.ToString(),i);
            }
            int w=5;
            mainBlock.AddOnEnd(filename,w,w.ToString(),w.ToString(),w.ToString(),w);
            mainBlock.AddOnEnd(filename,7,7.ToString(),7.ToString(),7.ToString(),7);
            w=9;
            mainBlock.AddOnEnd(filename,w,w.ToString(),w.ToString(),w.ToString(),w);
            w=8;
            mainBlock.AddOnEnd(filename,w,w.ToString(),w.ToString(),w.ToString(),w);
            w=4;
            mainBlock.AddOnEnd(filename,w,w.ToString(),w.ToString(),w.ToString(),w);*/
            string a="";
            while (a!="9"){
                fileSize.Refresh();
                Console.WriteLine("Размер файла = "+fileSize.Length);
                Console.Write("\n1-Осуществление добавления информации о студенте\n2-Осуществление изменения информации о студенте\n3-Осуществление удаление информации о студенте\n4-Осуществление поиска информации о студенте\nВвод: ");
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
                            Console.Write("Введите номер зачётки сткденкта которого хотиие изменить: ");
                            int oldidz = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("На что засменяем: ");
                            Console.Write("Номер зачётки: ");
                            int idZ = Convert.ToInt32(Console.ReadLine());
                            if(idZ==0){
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
                            mainBlock.Edit(filename,oldidz, idZ,lastname,name,middlename,idG);
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
                                mainBlock.PrintFindStudent(idZ%4);
                            }
                            else{
                                Console.WriteLine("\nУпс, ничего не удалось найти");
                            }
                            break;
                        }
                        case "5":
                            {
                                mainBlock.test11(filename);
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
