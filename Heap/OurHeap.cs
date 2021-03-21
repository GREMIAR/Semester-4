using System;
using System.IO;
using System.Text;
using System.Linq;
namespace BDlab1{
    partial class OurBlock{
        Block block = new Block();
        public OurBlock(){}
        
        void ByteArrToBlock(byte[] blockBinary)
        {
            byte[] byteArrByf = new byte[30];
            byte[] intArr = new byte[4];
            int r1,r5;
            char[] r2 = new char[30];
            char[] r3 = new char[20];
            char[] r4 = new char[30];
            for (int i=0;i<440;i+=88)
            {
                Array.Copy(blockBinary,i,intArr,0,4);
                r1 = BitConverter.ToInt32(intArr, 0);
                Array.Copy(blockBinary,i+4,byteArrByf,0,30);
                r2 = Encoding.UTF8.GetChars(byteArrByf);
                Array.Copy(blockBinary,i+34,byteArrByf,0,20);
                r3 = Encoding.UTF8.GetChars(byteArrByf);
                Array.Copy(blockBinary,i+54,byteArrByf,0,30);
                r4 = Encoding.UTF8.GetChars(byteArrByf);
                Array.Copy(blockBinary,i+84,intArr,0,4);
                r5 = BitConverter.ToInt32(intArr, 0);
                block.SetZapMass(i/88,r1,r2,r3,r4,r5);
            }
            return; 
        }
        
        int FindStudent(int idRecordBook){
            for(int i=0;i<5;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==idRecordBook)
                {
                    return i;
                }
            }
            return -1;
        }

        public void PrintFindStudent(int i)
        {
            Console.WriteLine("Студент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
            block.GetZapMass(i).GetIdRecordBook(), InString(block.GetZapMass(i).GetLastname(),30), InString(block.GetZapMass(i).GetName(),20), 
            InString(block.GetZapMass(i).GetMiddlename(),30), block.GetZapMass(i).GetIdGroup());
        }

        char[] ByteChar(BinaryReader reader,int length)
        {
            string charArr = "";
            for (int i=0;i<length;i++)
            {
                charArr+=reader.ReadChar();          
                if(charArr[i]=='\0'){
                    for(int f=i+1;f<length;f++)
                    {
                        reader.ReadChar();
                    }
                    break;
                }                 
            }
            return StringinChar(charArr);
        }
        
        char[] StringinChar(string str){
            char[] newChar = new char[str.Length-1];
            for (int i=0;i<str.Length-1;i++){
                newChar[i]=str[i];
            }
            return newChar;
        }
        
        public void PrintBlock(){
            Console.WriteLine("\n----Весь Блок----");
            for(int i = 0; i < 5; i++){
                if(block.GetZapMass(i).GetIdRecordBook()==0)
                {
                    Console.WriteLine("Номер записи в блоке: {0}; Пусто",i+1);
                }
                else
                {
                    Console.WriteLine("Номер записи в блоке: {0}; Номер зачётки: {1}; Фамилия: {2}; Имя: {3}; Отчесвто: {4}; Номер группы: {5};",i+1,block.GetZapMass(i).GetIdRecordBook(),
                    InString(block.GetZapMass(i).GetLastname(),30),InString(block.GetZapMass(i).GetLastname(),20),InString(block.GetZapMass(i).GetMiddlename(),30),block.GetZapMass(i).GetIdGroup());
                }
            }
            Console.WriteLine();
        }
        
        char[] InChar(string str, int length)
        {
            char[] charArr = new char[length];
            for (int i = 0; i < length&&str.Length > i; i++)
            {
                charArr[i] = str[i];
            }
            return charArr;
        }
        
        string InString(char[] charArr, int length)
        {
            string str="";
            try{
                for (int i = 0; i < length-1; i++)
                {
                    if(charArr[i]=='\0')
                    {
                        break;
                    }
                    str+= charArr[i];

                }
            }
            catch (Exception)
            {
                return str;
            }
            return str;
        }
        
        byte[] Combine(Zap zap)
        {
            byte[] idRecordBookB = BitConverter.GetBytes(zap.GetIdRecordBook());
            byte[] lastnameB = Encoding.UTF8.GetBytes(zap.GetLastname());
            byte[] nameB = Encoding.UTF8.GetBytes(zap.GetName());
            byte[] middlenameB = Encoding.UTF8.GetBytes(zap.GetMiddlename());
            byte[] idIdGroupB = BitConverter.GetBytes(zap.GetIdGroup());
            return idRecordBookB.Concat(lastnameB.Concat(nameB.Concat(middlenameB.Concat(idIdGroupB)))).ToArray();
        }
        
        byte[] Combine(byte[] first, byte[] second)
        {
            return first.Concat(second).ToArray();
        }

        int ReadNullBlockInt(BinaryReader reader){  
           
            try{  
                int size = reader.ReadInt32();  
                return size;  
            }
            catch(IOException e){
                Console.WriteLine("Исключение при чтении нулевого блока: " + e);
            }  
            reader.Close();
            return -1;
        }

        int ReadNullBlockInt(string filename){  
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                try{  
                    int size = reader.ReadInt32();  
                    return size;  
                }
                catch(IOException e){
                    Console.WriteLine("Исключение при чтении нулевого блока: " + e);
                }  
                reader.Close();
                return -1;
            }
        }

    }
}
