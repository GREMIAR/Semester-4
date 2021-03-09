using System;
using System.IO;
using System.Text;
namespace BDlab1{
    internal class Function
    {
        public static char[] InChar(string str, int length)
        {
            char[] charArr = new char[length];
            for (int i = 0; i < length&&str.Length > i; i++)
            {
                charArr[i] = str[i];
            }
            return charArr;
        }

        public static char[] ByteChar(BinaryReader reader,int length)
        {
            string arrtest = "";
            for (int i=0;i<length;i++)
            {
                arrtest+=reader.ReadChar();          
                if(arrtest[i]=='\0'){
                    for(int f=i+1;f<length;f++)
                    {
                        reader.ReadChar();
                    }
                    break;
                }                 
            }
            return StringinChar(arrtest);
        }

        public static void AddInfoAboutStudent_OnEnd(string namefile, int idRecordBook,string lastname,string name,string middlename,int idGroup)
        {
            char [] arr1 = InChar(lastname,30);
            char [] arr2 = InChar(name,20);
            char [] arr3 = InChar(middlename,30);
            Zap zaptest = new Zap(idRecordBook,arr1,arr2,arr3,idGroup);
            using (BinaryWriter writer = new BinaryWriter(File.Open(namefile, FileMode.OpenOrCreate)))
            {
                writer.Seek(0,SeekOrigin.End);
                writer.Write(zaptest.GetIdRecordBook());
                writer.Write(zaptest.GetLastname());
                writer.Write(zaptest.GetName());
                writer.Write(zaptest.GetMiddlename());
                writer.Write(zaptest.GetIdGroup());
                writer.Close();
            }
        }

        public static char[] StringinChar(string str){
            char[] charLast = new char[str.Length-1];
            for (int i=0;i<str.Length-1;i++){
                charLast[i]=str[i];
            }
            return charLast;
        }

        public static void PrintBlock(Block Arr){
            Console.WriteLine("----Весь Блок----\n");
            for(int i = 0; i <= Arr.GetSize(); i++){
                Console.WriteLine("Номер блока: {0}; Номер зачётки: {1}; Фамилия: {2}; Имя: {3}; Отчество: {4}; Номер группы: {5};",i+1,
                Arr.GetZapMass(i).GetIdRecordBook(), new string(Arr.GetZapMass(i).GetLastname()), new string(Arr.GetZapMass(i).GetName()), 
                new string(Arr.GetZapMass(i).GetMiddlename()), Arr.GetZapMass(i).GetIdGroup());
            }
            Console.WriteLine();
        }

        public static void Search(int idRecordBook,string namefile){
            Block blockZap = new Block();
            using (BinaryReader reader = new BinaryReader(File.Open(namefile, FileMode.Open)))
            {
                Zap[] arrStu = new Zap[5];
                while(reader.PeekChar()!=-1)
                {
                    for(int i=0;i<5;i++)
                    {
                        arrStu[i] = new Zap(reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                        if (reader.PeekChar()==-1)
                        {
                            blockZap = new Block(arrStu);
                            blockZap.SetSize(i);
                            if(!Student(blockZap,idRecordBook))
                            {
                                Console.WriteLine("\nУпс, ничего не удалось найти\n");
                            }
                            reader.Close();
                            return;
                        }
                    }
                    blockZap = new Block(arrStu);
                    if(Student(blockZap,idRecordBook))
                    {
                        reader.Close();
                        return;
                    }

                }
            }
        }

        public static bool Student(Block blockNew,int idRecordBook){
            for(int i=0;i<=blockNew.GetSize();i++)
            {
                if(blockNew.GetZapMass(i).GetIdRecordBook()==idRecordBook){
                    PrintBlock(blockNew);
                    Console.WriteLine("Студент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
                    blockNew.GetZapMass(i).GetIdRecordBook(), new string(blockNew.GetZapMass(i).GetLastname()), new string(blockNew.GetZapMass(i).GetName()), 
                    new string(blockNew.GetZapMass(i).GetMiddlename()), blockNew.GetZapMass(i).GetIdGroup());
                    return true;
                }
            }
            return false;
        }

        public static void Edit()
        {

        }
        public static void Remove(int idRecordBook,string namefile)
        {

        }
    }

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
                        AddInfoAboutStudent_OnEnd(namefile, idZ,lastname,name,midlename,idG);
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