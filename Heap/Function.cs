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
        public static void AddOnEnd(BinaryWriter writer,string filename, int idRecordBook,string lastname,string name,string middlename,int idGroup)
        {
            char [] lastnameChar = InChar(lastname,30);
            char [] nameChar = InChar(name,20);
            char [] middlenameChar = InChar(middlename,30);
            Zap zapArr = new Zap(idRecordBook,lastnameChar,nameChar,middlenameChar,idGroup);
            using (writer)
            {
                //writer.Seek(0,SeekOrigin.Begin);
                writer.Write(ReadNullBlock(filename)+1);
                writer.Seek(0,SeekOrigin.End);
                writer.Write(zapArr.GetIdRecordBook());
                writer.Write(zapArr.GetLastname());
                writer.Write(zapArr.GetName());
                writer.Write(zapArr.GetMiddlename());
                writer.Write(zapArr.GetIdGroup());
            }
        }
        public static void Edit(string filename)
        {
            Console.WriteLine(ReadNullBlock(filename));
           /* using (FileStream file1 = new FileStream("BD.bin", FileMode.Open))
            {
                byte [] buffer = new byte[10];
                file1.Seek(-84,SeekOrigin.End);
                file1.Read(buffer, 0, buffer.Length);
                Console.WriteLine(Convert.ToString(buffer));
            }*/
        }
        public static void Remove(int idRecordBook,string filename)
        {
           /* Block blockArr = new Block();
            int numBlock=0,numZap=1;
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
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
        public static int ReadNullBlock(string filename){  
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                try{  
                    return reader.ReadInt32();  
                }
                catch(IOException e){
                    Console.WriteLine("Exception on reading zero block: " + e);
                }  
                return -1;
            }
        }
    }
}