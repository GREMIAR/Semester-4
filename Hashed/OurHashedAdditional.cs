using System;
using System.IO;
using System.Text;
using System.Linq;
namespace Hashed{
    partial class OurBlock{

        public OurBlock(){}

        int HashFunction(int num)
        {
            return num%4;
        }

        void ByteArrToBlock(byte[] blockBinary)
        {
            byte[] byteArrByf30 = new byte[30];
            byte[] byteArrByf20 = new byte[20];
            byte[] intArrB = new byte[4];
            int idRB,idG,nextB;
            char[] lastName = new char[30];
            char[] name = new char[20];
            char[] patronymic = new char[30];
            for (int i=0;i<blockSize-4;i+=(blockSize-4)/5)
            {
                Array.Copy(blockBinary,i,intArrB,0,4);
                idRB = BitConverter.ToInt32(intArrB, 0);
                Array.Copy(blockBinary,i+4,byteArrByf30,0,30);
                lastName = Encoding.UTF8.GetChars(byteArrByf30);
                Array.Copy(blockBinary,i+34,byteArrByf20,0,20);
                name = Encoding.UTF8.GetChars(byteArrByf20);
                Array.Copy(blockBinary,i+54,byteArrByf30,0,30);
                patronymic  = Encoding.UTF8.GetChars(byteArrByf30);
                Array.Copy(blockBinary,i+84,intArrB,0,4);
                idG = BitConverter.ToInt32(intArrB, 0);
                block.SetZapMass(i/((blockSize-4)/5),idRB,lastName,name,patronymic,idG);
            }
            Array.Copy(blockBinary,blockSize-4,intArrB,0,4);
            nextB = BitConverter.ToInt32(intArrB,0);
            block.SetNextb(nextB); 
        }

        public void ReadFullNullBlock(byte[] nullBlockBinary)
        {
            byte[] intArrB = new byte[4];
            int[] intArr = new int[9];
            for(int i=0;i<9;i++)
            {
                Array.Copy(nullBlockBinary,i*4,intArrB,0,4);
                intArr[i] = BitConverter.ToInt32(intArrB, 0);
            }
            nullBlock = new NullBlock(intArr[0],intArr[1],intArr[2],intArr[3],intArr[4],intArr[5],intArr[6],intArr[7],intArr[8]);
        }

        int FindStudent(int idRecordBook){
            for(int i=0;i<5;i++)
            {
                if(block.GetZapMass(i).IdRecordBook==idRecordBook)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Reset(string filename,int oldidRecordBook)
        {
            int quantityBlock = nullBlock.QuantityBlock;
            int addrDelBlock = SearchMid(oldidRecordBook,filename);
            int i;
            for(i=0;i<5;i++)
            {
                if(block.GetZapMass(i).IdRecordBook==oldidRecordBook)
                {
                    break;
                }
            }
            block.SetZapMass(i,0,InChar("0",30),InChar("0",20), InChar("0",30),0);
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(addrDelBlock,SeekOrigin.Begin);
                byte[] blockBinary = new byte[blockSize];
                blockBinary = Combine();
                writer.Write(blockBinary);
            }
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

        public void PrintBlock(){
            Console.WriteLine("\n----Весь Блок----");
            for(int i = 0; i < 5; i++){
                if(block.GetZapMass(i).IdRecordBook==0)
                {
                    Console.WriteLine("Номер записи в блоке: {0}; Пусто",i+1);
                }
                else
                {
                    Console.WriteLine("Номер записи в блоке: {0}; Номер зачётки: {1}; Фамилия: {2}; Имя: {3}; Отчесвто: {4}; Номер группы: {5};",i+1,block.GetZapMass(i).IdRecordBook,
                    InString(block.GetZapMass(i).Lastname,30),InString(block.GetZapMass(i).Lastname,20),InString(block.GetZapMass(i).Middlename,30),block.GetZapMass(i).IdGroup);
                }
            }
            Console.WriteLine();
        }

        public void PrintFindStudent(int i)
        {
            i = HashFunction(i);
            Console.WriteLine("Студент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
            block.GetZapMass(i).IdRecordBook, InString(block.GetZapMass(i).Lastname,30), InString(block.GetZapMass(i).Name,20), 
            InString(block.GetZapMass(i).Middlename,30), block.GetZapMass(i).IdGroup);
        }

        byte[] Combine(Zap zap)
        {
            byte[] idRecordBookB = BitConverter.GetBytes(zap.IdRecordBook);
            byte[] lastnameB = Encoding.UTF8.GetBytes(zap.Lastname);
            byte[] nameB = Encoding.UTF8.GetBytes(zap.Name);
            byte[] middlenameB = Encoding.UTF8.GetBytes(zap.Middlename);
            byte[] idIdGroupB = BitConverter.GetBytes(zap.IdGroup);
            return idRecordBookB.Concat(lastnameB.Concat(nameB.Concat(middlenameB.Concat(idIdGroupB)))).ToArray();
        }

        byte[] Combine(byte[] start, byte[] second)
        {
            return start.Concat(second).ToArray();
        }

        byte[] Combine()
        {
            byte[] byteBlock = new byte[0];
            for(int i = 0;i<5;i++)
            {
                byte[] byteZap=Combine(block.GetZapMass(i));
                byteBlock=Combine(byteBlock,byteZap);
            }
            byte[] nextB = BitConverter.GetBytes(block.Nextb);
            return Combine(byteBlock,nextB);
        }

        void AddZapOnEnd(int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            for(int i=0;i<5;i++)
            {
                if(block.GetZapMass(i).IdRecordBook==0)
                {
                    block.SetZapMass(i,idRecordBook,InChar(lastname,30),InChar(name,20),InChar(patronymic,30),idGroup);
                    break;
                }
            }
        }

        public int SearchMid(int idRecordBook,string filename)
        {
            Mid.idZ=idRecordBook;
            int idRBHashed = HashFunction(idRecordBook);
            int quantityBlock = nullBlock.QuantityBlock;
            int start = nullBlock.GetPointersStart(idRBHashed);
            int end = nullBlock.GetPointersEnd(idRBHashed);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                int numZapFound;
                int backAddr=0;
                int temp=0;
                while(start!=0)
                {   
                    reader.Seek(start, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        Mid.back=backAddr;
                        Mid.addr=start;
                        Mid.next=block.Nextb;
                        if(temp==0)
                        {
                            Mid.start=true;
                        }
                        if(block.Nextb==0)
                        {
                            Mid.end=true;
                        }
                        reader.Close();
                        return start;
                    }
                    backAddr=start;
                    start=block.Nextb;
                    temp+=1;
                }

            }
            return -1;
        }

        public int SearchBack(int idRecordBook,string filename)
        {
            Back.idZ=idRecordBook;

            int idRBHashed = HashFunction(idRecordBook);
            int quantityBlock = nullBlock.QuantityBlock;
            int start = nullBlock.GetPointersStart(idRBHashed);
            int end = nullBlock.GetPointersEnd(idRBHashed);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                int numZapFound;
                int backAddr=0;
                int temp=0;
                while(start!=0)
                {
                    reader.Seek(start, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    
                    ByteArrToBlock(blockBinary);

                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        Back.back=backAddr;
                        Back.addr=start;
                        Back.next=block.Nextb;
                        if(temp==0)
                        {
                            Back.start=true;
                        }
                        if(block.Nextb==0)
                        {
                            Back.end=true;
                        }
                        reader.Close();
                        return start;
                    }
                    backAddr=start;
                    start=block.Nextb;
                    temp+=1;
                }

            }
            return -1;
        }

        bool CheckLastBlock(string filename,int end)
        {
            if(end==0)
            {
                return false;
            }
            byte[] blockBinary = new byte[blockSize];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek(end, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, blockSize);
                ByteArrToBlock(blockBinary);
                if(FindStudent(0)!=-1)
                {
                    reader.Close();
                    return true;
                }
            }
            return false;
        }

        public int SearchEndCheck(int idRecordBook,string filename)
        {
            int idRBHashed = HashFunction(idRecordBook);
            int quantityBlock = nullBlock.QuantityBlock;
            int start = nullBlock.GetPointersStart(idRBHashed);
            int end = nullBlock.GetPointersEnd(idRBHashed);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                int numZapFound;
                while(start!=0)
                {
                    reader.Seek(start, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return numZapFound;
                    }
                    start=block.Nextb;
                }
                if (FindStudent(0)==-1)
                {
                    return -1;
                }
            }
            
            return -2;
        }
        int ReadNullBlock(string filename){  
            byte[] blockBinary = new byte[4];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek(0, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, 4);
            }
            return BitConverter.ToInt32(blockBinary, 0);
        }

        int ReadFirstBlock(string filename,int i)
        {
            byte[] blockBinary = new byte[4];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek(4+i*8, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, 4);
            }
            return BitConverter.ToInt32(blockBinary, 0);
        }

        int ReadEndBlock(string filename,int i)
        {
            byte[] blockBinary = new byte[4];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek(4+i*8+4, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, 4);
            }
            return BitConverter.ToInt32(blockBinary, 0);
        }
    }

    
}
