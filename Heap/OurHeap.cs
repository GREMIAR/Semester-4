using System;
using System.IO;
using System.Text;

//-1 в зачётку занести надо если нет ничего 
namespace BDlab1{
class OurBlock{
        Block block = new Block();
        public OurBlock(){}
        public int Search(int idRecordBook,string filename)
        {
            FileInfo file = new FileInfo(filename);
            Console.WriteLine(file.Length);
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                byte[] blockBinary = new byte[440];
                int numBlock = Function.ReadNullBlockInt(reader);
                int numZapFound;
                for(int i=0;i<numBlock;i++)
                {
                    block.SetFullRestart();
                    reader.Read(blockBinary, 0, 440);
                    ByteArrToBlock(blockBinary);
                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return (numZapFound)*88;
                    }
                }
                reader.Close();
                return -1;
            }
        }
        void ByteArrToBlock(byte[] blockBinary)
        {
            byte[] byteArrByf = new byte[30];
            byte[] intArr = new byte[4];
            int r1;
            char[] r2 = new char[30];
            char[] r3 = new char[20];
            char[] r4 = new char[30];
            int r5;
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
                if(r1!=-1)
                {
                    block.SetZapMass(i/88,r1,r2,r3,r4,r5);
                }
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
            Console.WriteLine("\nСтудент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
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
            Console.WriteLine("----Весь Блок----\n");
            for(int i = 0; i < 5; i++){
                Console.WriteLine(block.GetZapMass(i).GetIsZap());
                if(block.GetZapMass(i).GetIsZap())
                {
                    Console.WriteLine("Номер записи в блоке: {0}; Номер зачётки: {1}; Фамилия: {2}; Имя: {3}; Отчесвто: {4}; Номер группы: {5};",i+1,block.GetZapMass(i).GetIdRecordBook(),
                    InString(block.GetZapMass(i).GetLastname(),30),InString(block.GetZapMass(i).GetLastname(),20),InString(block.GetZapMass(i).GetMiddlename(),30),block.GetZapMass(i).GetIdGroup());
                }
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
            int sizeZapLast = Function.ReadNullBlockInt(filename);
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                int zapLastItem;
                if((zapLastItem = Search(-1,filename))==-1){//есть свободное место в блоке
                    writer.Seek(sizeZapLast,SeekOrigin.Begin);
                    sizeZapLast--;
                    sizeZapLast*=440;
                    sizeZapLast+=4;
                    sizeZapLast+=88*zapLastItem;
                    writer.Seek(sizeZapLast,SeekOrigin.Begin);
                    writer.Write(block.GetZapMass(0).GetIdRecordBook());
                    writer.Write(block.GetZapMass(0).GetLastname());
                    writer.Write(block.GetZapMass(0).GetName());
                    writer.Write(block.GetZapMass(0).GetMiddlename());
                    writer.Write(block.GetZapMass(0).GetIdGroup());
                }
                else{
                    writer.Write(sizeZapLast+1);
                    sizeZapLast--;
                    sizeZapLast*=88;
                    sizeZapLast+=4;
                    writer.Seek(sizeZapLast,SeekOrigin.Begin);
                    writer.Write(block.GetZapMass(0).GetIdRecordBook());
                    writer.Write(block.GetZapMass(0).GetLastname());
                    writer.Write(block.GetZapMass(0).GetName());
                    writer.Write(block.GetZapMass(0).GetMiddlename());
                    writer.Write(block.GetZapMass(0).GetIdGroup());
                }
            }
            char [] lastnameChar = InChar(lastname,30);
            char [] nameChar = InChar(name,20);
            char [] patronymicChar = InChar(patronymic,30);
            for(int i=0;i<5;i++)
            {
                if(block.GetZapMass(i).GetIsZap()==false)
                {
                    block.SetZapMass(i,idRecordBook,lastnameChar,nameChar,patronymicChar,idGroup);
                    break;
                }
            }
            
            //int sizeZap = Function.ReadNullBlockInt(filename);
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