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
            char[] charLast = new char[str.Length];
            for (int i=0;i<str.Length;i++){
                charLast[i]=str[i];
            }
            return charLast;
        }

        public static Block OutputBlock(string namefile){
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
                            reader.Close();
                            return blockZap;
                        }
                    }

                }
            }
            return blockZap;
        }
        public static void PrintBlock(Block Arr){

            for(int i = 0; i <= Arr.GetSize(); i++){
                string s1 = new string(Arr.GetZapMass(i).GetLastname());
                string s2 = new string(Arr.GetZapMass(i).GetName());
                string s3 = new string(Arr.GetZapMass(i).GetMiddlename());
                Console.WriteLine("Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};",
                Arr.GetZapMass(i).GetIdRecordBook(), s1, s2, s3, Arr.GetZapMass(i).GetIdGroup());
            }
        }
    }

    class Program : Function
    {
        static void Main(string[] args){
            const string namefile = "BD.bin";
            string a="";
            
            while (a!="9"){
                Console.WriteLine("1-/2-/3-/4-");
                a=Console.ReadLine();
                if(a=="1")
                {
                    Console.WriteLine("Зачётку");
                    int idZ = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Фамилию");
                    string lastname = Console.ReadLine();
                    Console.WriteLine("Имя");
                    string name = Console.ReadLine();
                    Console.WriteLine("Отчество");
                    string midlename = Console.ReadLine();
                    Console.WriteLine("Группа");
                    int idG = Convert.ToInt32(Console.ReadLine());
                    AddInfoAboutStudent_OnEnd(namefile, idZ,lastname,name,midlename,idG);
                }
                else if(a=="2")
                {
                    PrintBlock(OutputBlock(namefile)); 
                }
                else if(a=="3")
                {

                }
                else if(a=="4")
                {

                }
            }
        }
    }
}