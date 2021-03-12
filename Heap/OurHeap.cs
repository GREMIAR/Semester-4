using System;
using System.IO;
namespace BDlab1{
class OurBlock{
        Block block = new Block();
        public OurBlock(){}
        public int Search(int idRecordBook,string filename)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int numZap = reader.ReadInt32();
                int i=1,f;
                int numZapFound;
                while(numZap>i)
                {
                    for(f=0;f<5;f++)
                    {
                        if(i>=numZap)
                        {
                            break;    
                        }
                        block.SetZapMass(f,reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                        i++;
                    }
                    block.SetSize(f);
                    if((numZapFound=FindStudent(idRecordBook,f))!=-1)
                    {
                        reader.Close();
                        return (numZapFound)*88;
                    }
                }
                reader.Close();
                return -1;
            }
        }
        int FindStudent(int idRecordBook,int numZip){
            for(int i=0;i<numZip;i++)
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
            Console.WriteLine("\nСтудент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
            block.GetZapMass(i).GetIdRecordBook(), new string(block.GetZapMass(i).GetLastname()), new string(block.GetZapMass(i).GetName()), 
            new string(block.GetZapMass(i).GetMiddlename()), block.GetZapMass(i).GetIdGroup());
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
            Console.WriteLine("----Весь Блок----\n");
            for(int i = 0; i < block.GetSize(); i++){
                Console.Write("Номер записи в блоке: {0}; Номер зачётки: {1};",i+1,block.GetZapMass(i).GetIdRecordBook());
                Console.Write("Фамилия: ");
                Console.Write(new string(block.GetZapMass(i).GetLastname()));
                Console.Write("; Имя: ");
                Console.Write(new string(block.GetZapMass(i).GetName()));
                Console.Write("; Отчество: ");
                Console.Write(new string(block.GetZapMass(i).GetMiddlename()));
                Console.WriteLine("; Номер группы: {0};",block.GetZapMass(i).GetIdGroup());
            }
            Console.WriteLine();
        }
        public int Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup)
        {
            int numZap=Search(oldidRecordBook,filename);
            if(numZap==-1)
            {
                return -1;
            }
            numZap+=4;
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(numZap,SeekOrigin.Begin);
                writer.Write(idRecordBook);
                writer.Write(InChar(lastname,30));
                writer.Write(InChar(name,20));
                writer.Write(InChar(patronymic,30));
                writer.Write(idGroup); 
                writer.Close();              
            }
            Search(idRecordBook,filename);
            return 0;
        }
        static char[] InChar(string str, int length)
        {
            char[] charArr = new char[length];
            for (int i = 0; i < length&&str.Length > i; i++)
            {
                charArr[i] = str[i];
            }
            return charArr;
        }
        public static string InString(char[] charArr, int length)
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
        public void Remove(int idRecordBook,string filename)
        {
            int sizeZap;
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                sizeZap = reader.ReadInt32();
                if(sizeZap==1)
                {
                    Console.WriteLine("Мы ничего не можем удалить, так как БД пуста");
                    return;
                }  
                for(int i=0;i<sizeZap-1;i++)
                {
                    block.SetZapMass(i%5,reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                }
                reader.Close();
            }
            if(Edit(filename,idRecordBook,block.GetZapMass((sizeZap-2)%5).GetIdRecordBook(), InString(block.GetZapMass((sizeZap-2)%5).GetLastname(),30),InString(block.GetZapMass((sizeZap-2)%5).GetName(),20),InString(block.GetZapMass((sizeZap-2)%5).GetMiddlename(),30),block.GetZapMass((sizeZap-2)%5).GetIdGroup())==-1)
            {
                return;
            }
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Write(sizeZap-1);
                writer.Close();
            }
        }
        public void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            if(Search(idRecordBook,filename)!=-1){
                return;
            }
            char [] lastnameChar = OurBlock.InChar(lastname,30);
            char [] nameChar = OurBlock.InChar(name,20);
            char [] patronymicChar = OurBlock.InChar(patronymic,30);
            block.SetZapMass(0,idRecordBook,lastnameChar,nameChar,patronymicChar,idGroup);
            int sizeZap = Function.ReadNullBlockInt(filename);
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Write(sizeZap+1);
                sizeZap--;
                sizeZap*=88;
                sizeZap+=4;
                writer.Seek(sizeZap,SeekOrigin.Begin);
                writer.Write(block.GetZapMass(0).GetIdRecordBook());
                writer.Write(block.GetZapMass(0).GetLastname());
                writer.Write(block.GetZapMass(0).GetName());
                writer.Write(block.GetZapMass(0).GetMiddlename());
                writer.Write(block.GetZapMass(0).GetIdGroup());
            }
        }
        public int Unique(string filename,int findIdRecordBook)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int size = reader.ReadInt32();
                for(int i=0;i<size-1;i++)
                {
                    block.SetZapMass(i%5,reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                    if(block.GetZapMass(i%5).GetIdRecordBook()==findIdRecordBook)
                    {
                        Console.WriteLine("\nС таким номером зачётки студент уже есть");
                        return -1;
                    }
                }
                reader.Close();
                return 0;
            }
        }

    }
}