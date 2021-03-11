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
                    if((numZapFound=Student(idRecordBook,f))!=-1)
                    {
                        reader.Close();
                        return (numZapFound)*88;
                    }
                }
                reader.Close();
                Console.WriteLine("\nУпс, ничего не удалось найти\n");
                return -1;
            }
        }

        int Student(int idRecordBook,int numZip){
            for(int i=0;i<numZip;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==idRecordBook)
                {
                    PrintBlock();
                    Console.WriteLine("Студент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
                    block.GetZapMass(i).GetIdRecordBook(), new string(block.GetZapMass(i).GetLastname()), new string(block.GetZapMass(i).GetName()), 
                    new string(block.GetZapMass(i).GetMiddlename()), block.GetZapMass(i).GetIdGroup());
                    return i;
                }
            }
            return -1;
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
        void PrintBlock(){
            Console.WriteLine("----Весь Блок----\n");
            for(int i = 0; i < block.GetSize(); i++){
                Console.WriteLine("Номер записи в блоке: {0}; Номер зачётки: {1}; Фамилия: {2}; Имя: {3}; Отчество: {4}; Номер группы: {5};",i+1,
                block.GetZapMass(i).GetIdRecordBook(), new string(block.GetZapMass(i).GetLastname()), new string(block.GetZapMass(i).GetName()), 
                new string(block.GetZapMass(i).GetMiddlename()), block.GetZapMass(i).GetIdGroup());
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
            Console.WriteLine("Заменён на: \n");
            Search(idRecordBook,filename);
            return 0;
        }
        public static char[] InChar(string str, int length)
        {
            char[] charArr = new char[length];
            for (int i = 0; i < length&&str.Length > i; i++)
            {
                charArr[i] = str[i];
            }
            return charArr;
        }
        public void Remove(int idRecordBook,string filename)
        {
            int sizeZap;
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                sizeZap = reader.ReadInt32();  
                Console.WriteLine(sizeZap);
                for(int i=0;i<sizeZap-1;i++)
                {
                    block.SetZapMass(i%5,reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                }
                reader.Close();
            }
            if(Edit(filename,idRecordBook,block.GetZapMass((sizeZap-2)%5).GetIdRecordBook(), block.GetZapMass((sizeZap-2)%5).GetLastname().ToString(),block.GetZapMass((sizeZap-2)%5).GetName().ToString(),block.GetZapMass((sizeZap-2)%5).GetMiddlename().ToString(),block.GetZapMass((sizeZap-2)%5).GetIdGroup())==-1)
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

    }
}