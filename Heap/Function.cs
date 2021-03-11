using System;
using System.IO;
using System.Text;
namespace BDlab1{    
    internal class Function  
    {
        public static void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            char [] lastnameChar = OurBlock.InChar(lastname,30);
            char [] nameChar = OurBlock.InChar(name,20);
            char [] patronymicChar = OurBlock.InChar(patronymic,30);
            Zap zapArr = new Zap(idRecordBook,lastnameChar,nameChar,patronymicChar,idGroup);
            int i = ReadNullBlockInt(filename)+1;
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Write(i);
                writer.Seek(0,SeekOrigin.End);
                writer.Write(zapArr.GetIdRecordBook());
                writer.Write(zapArr.GetLastname());
                writer.Write(zapArr.GetName());
                writer.Write(zapArr.GetMiddlename());
                writer.Write(zapArr.GetIdGroup());
            }
        }
        public static int ReadNullBlockInt(string filename){  
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                try{  
                    int size = reader.ReadInt32();  
                    reader.Close();
                    return size;  
                }
                catch(IOException e){
                    Console.WriteLine("Exception on reading zero block: " + e);
                }  
                reader.Close();
                return -1;
            }
        }
        public static BinaryReader ReadNullBlock(BinaryReader reader){  
            try{  
                int size = reader.ReadInt32();  
                reader.Close();
                return reader;  
            }
            catch(IOException e){
                Console.WriteLine("Exception on reading zero block: " + e);
            }  
            reader.Close();
            return reader;
        }
    }
}