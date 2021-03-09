using System;
using System.IO;
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

        public static void AddOnEnd(string namefile, int idRecordBook,string lastname,string name,string middlename,int idGroup)
        {
            char [] lastnameChar = InChar(lastname,30);
            char [] nameChar = InChar(name,20);
            char [] middlenameChar = InChar(middlename,30);
            Zap zapArr = new Zap(idRecordBook,lastnameChar,nameChar,middlenameChar,idGroup);
            using (BinaryWriter writer = new BinaryWriter(File.Open(namefile, FileMode.OpenOrCreate)))
            {
                writer.Seek(0,SeekOrigin.End);
                writer.Write(zapArr.GetIdRecordBook());
                writer.Write(zapArr.GetLastname());
                writer.Write(zapArr.GetName());
                writer.Write(zapArr.GetMiddlename());
                writer.Write(zapArr.GetIdGroup());
                writer.Close();
            }
        }

        public static char[] StringinChar(string str){
            char[] newChar = new char[str.Length-1];
            for (int i=0;i<str.Length-1;i++){
                newChar[i]=str[i];
            }
            return newChar;
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

        public static int Search(int idRecordBook,string namefile){
            Block blockArr = new Block();
            int numBlock=0,numZap=1;
            using (BinaryReader reader = new BinaryReader(File.Open(namefile, FileMode.Open)))
            {
                Zap[] zapArr = new Zap[5];
                while(reader.PeekChar()!=-1)
                {
                    for(int i=0;i<5;i++)
                    {
                        zapArr[i] = new Zap(reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                        if (reader.PeekChar()==-1)
                        {
                            blockArr = new Block(zapArr);
                            blockArr.SetSize(i);
                            reader.Close();
                            if((numZap=Student(blockArr,idRecordBook))==-1)
                            {
                                Console.WriteLine("\nУпс, ничего не удалось найти\n");
                                return(-1);
                            }
                            return (84*(numBlock*5+numZap));
                        }
                    }
                    blockArr = new Block(zapArr);
                    if((numZap=Student(blockArr,idRecordBook))!=-1)
                    {
                        reader.Close();
                        return (84*(numBlock*5+numZap));;
                    }
                    numBlock++;

                }
            }
            return(-2);
        }


        public static int Student(Block blockNew,int idRecordBook){
            for(int i=0;i<=blockNew.GetSize();i++)
            {
                if(blockNew.GetZapMass(i).GetIdRecordBook()==idRecordBook){
                    PrintBlock(blockNew);
                    Console.WriteLine("Студент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
                    blockNew.GetZapMass(i).GetIdRecordBook(), new string(blockNew.GetZapMass(i).GetLastname()), new string(blockNew.GetZapMass(i).GetName()), 
                    new string(blockNew.GetZapMass(i).GetMiddlename()), blockNew.GetZapMass(i).GetIdGroup());
                    return i;
                }
            }
            return -1;
        }

        public static void Edit(string namefile)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(namefile, FileMode.OpenOrCreate)))
            {
                writer.Seek(-84,SeekOrigin.End);
                for(int i=0;i<84;i++){
                    writer.Write('\0');
                }
            }
        }
        public static void Remove(int idRecordBook,string namefile)
        {
           /* Block blockArr = new Block();
            int numBlock=0,numZap=1;
            using (BinaryReader reader = new BinaryReader(File.Open(namefile, FileMode.Open)))
            {
                Zap[] zapArr = new Zap[5];
                while(reader.PeekChar()!=-1)
                {
                    for(int i=0;i<5;i++)
                    {
                        zapArr[i] = new Zap(reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                        if (reader.PeekChar()==-1)
                        {
                            blockArr = new Block(zapArr);
                            blockArr.SetSize(i);
                            reader.Close();
                            if((numZap=Student(blockArr,idRecordBook))==-1)
                            {
                                Console.WriteLine("\nУпс, ничего не удалось найти\n");
                                return;
                            }
                            return;
                        
                    }
                    blockArr = new Block(zapArr);
                    if((numZap=Student(blockArr,idRecordBook))!=-1)
                    {
                        reader.Close();
                        return;
                    }
                    numBlock++;

                }
            }
            return;
        }*/
        }
    }
}