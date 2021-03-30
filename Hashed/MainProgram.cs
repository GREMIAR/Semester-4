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
            byte[] nullBlockBinary = new byte[36];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Read(nullBlockBinary, 0, nullBlockBinary.Length);
            }
            mainBlock.ReadFullNullBlock(nullBlockBinary);
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
                                Console.WriteLine("Нельзя присвоить этот номер зачётки!");
                                break;
                            }
                            int searchEndCheckResult= mainBlock.SearchEndCheck(idZ,filename);
                            if(searchEndCheckResult!=-1&&searchEndCheckResult!=-2){
                                Console.WriteLine("Номер зачётки {0} занят!",idZ);
                                break;
                            }
                            Console.Write("Фамилия: ");
                            string lastname = Console.ReadLine();
                            Console.Write("Имя: ");
                            string name = Console.ReadLine();
                            Console.Write("Отчество: ");
                            string patronymic = Console.ReadLine();
                            Console.Write("Номер группа: ");
                            int idG = Convert.ToInt32(Console.ReadLine());
                            mainBlock.AddOnEnd(filename, idZ,lastname,name,patronymic,idG,searchEndCheckResult);
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
                                Console.WriteLine("Нельзя присвоить этот номер зачётки!");
                                break;
                            }
                            int searchEndCheckResult= mainBlock.SearchEndCheck(idZ,filename);
                            if(searchEndCheckResult!=-1&&searchEndCheckResult!=-2){
                                Console.WriteLine("Номер зачётки {0} занят!",idZ);
                                break;
                            }
                            Console.Write("Фамилия: ");
                            string lastname = Console.ReadLine();
                            Console.Write("Имя: ");
                            string name = Console.ReadLine();
                            Console.Write("Отчество: ");
                            string patronymic = Console.ReadLine();
                            Console.Write("Номер группа: ");
                            int idG = Convert.ToInt32(Console.ReadLine());
                            mainBlock.Edit(filename,oldidz, idZ,lastname,name,patronymic,idG,searchEndCheckResult);
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
                            if(mainBlock.Search(idZ,filename)==-1)
                            {
                                Console.WriteLine("Номер зачётки {0} занят!",idZ);
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
                                mainBlock.PrintFindStudent(idZ);
                            }
                            else{
                                Console.WriteLine("\nУпс, ничего не удалось найти!");
                            }
                            break;
                        }
                        case "5":
                        {
                            mainBlock.Debuging(filename);
                            break;
                        }
                        default:
                            break;
                    }
                }
                catch(System.OverflowException)
                {
                    Console.WriteLine("Вы ввели число за рамками возможного!");
                }
            }
        }
    }
}
