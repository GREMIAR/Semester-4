using System;
using System.IO;
namespace Heshed{
    partial class OurBlock{
        const int blockSize = 444;
        public int Search(int idRecordBook,string filename)
        {
            int idHashed = HashFunction(idRecordBook);
            int end = ReadEndBlock(filename,idHashed);
            int first = ReadFirstBlock(filename,idHashed);
            int size = ReadNullBlock(filename);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                int numZapFound;
                Console.WriteLine("f="+first);
                while(first!=0)
                {
                    Console.WriteLine(1);
                    reader.Seek(first, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);

                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return numZapFound;//return i*5+numZapFound;
                    }
                    first=block.GetNextb;
                }

            }
            return -1;





            /*using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                byte[] blockBinary = new byte[blockSize];
                


                int numBlock = ReadNullBlockSize(reader);
                
                int numZapFound;
                for(int i=0;i<numBlock;i++)
                {
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return i;
                    }
                }
                reader.Close();
                return -1;
            }*/
        }

        public bool Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup)
        {
            int numBlock;
            if((numBlock=Search(oldidRecordBook,filename))==-1){
                Console.WriteLine("Номера зачётки {0} нету",oldidRecordBook);
                return false;
            }
            numBlock=(numBlock-numBlock%5)/5;
            byte[] blockBinary = new byte[blockSize];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek((numBlock)*blockSize+4, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, blockSize);
            }
            ByteArrToBlock(blockBinary);
            for(int i=0;i<5;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==oldidRecordBook)
                {
                    block.SetZapMass(i,idRecordBook,InChar(lastname,30),InChar(name,20), InChar(patronymic,30),idGroup);
                }
            }
            blockBinary=Combine();
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek((numBlock)*blockSize+4,SeekOrigin.Begin);
                writer.Write(blockBinary);
            } 
            return true;
        }

        public void Remove(int idRecordBook,string filename)
        {
            int numBlock;
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                numBlock = reader.ReadInt32();
            }
            if(numBlock==0)
            {
                Console.WriteLine("В БД нечего нет");
                return;
            }
            byte[] blockBinary = new byte[blockSize];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek((numBlock-1)*blockSize+4, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, blockSize);
            }
            ByteArrToBlock(blockBinary);
            int i;
            for(i=0;i<5;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==0)
                {
                    break;
                }
            }
            i--;
            if(Edit(filename,idRecordBook,block.GetZapMass(i).GetIdRecordBook(),InString(block.GetZapMass(i).GetLastname(),30),InString(block.GetZapMass(i).GetName(),20),InString(block.GetZapMass(i).GetMiddlename(),30),block.GetZapMass(i).GetIdGroup())==false)
            {
                return;
            }
            if(i==0)
            {
                FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength((numBlock-1)*blockSize+4);
                fileStream.Close();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Write(numBlock-1);
                }
            }
            else
            {
                block.SetZapMass(i,0,InChar("0",30),InChar("0",20),InChar("0",30),0);
                blockBinary=Combine();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek((numBlock-1)*blockSize+4,SeekOrigin.Begin);
                    writer.Write(blockBinary);
                }
            }
        }

        public void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            int idHashed = HashFunction(idRecordBook);
            int size = ReadNullBlock(filename);
            int first = ReadFirstBlock(filename,idHashed);
            int end = ReadEndBlock(filename,idHashed);
            if (first==0)
            {
                
            }
           /* using (var reader = File.Open(filename, FileMode.Open))
            {

            }
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(0,SeekOrigin.Begin);
                writer.Write(size++);

                writer.Seek(idRecordBook%4*4+4,SeekOrigin.Begin);
                writer.Write(size++);
            } */
            if(CheckLastBlock(filename,end)){
                byte[] blockBinary = new byte[blockSize];
                using (var reader = File.Open(filename, FileMode.Open))
                {
                    reader.Seek(end, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                }
                ByteArrToBlock(blockBinary);
                AddZapOnEnd(idRecordBook,lastname,name,patronymic,idGroup);
                blockBinary=Combine();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(end,SeekOrigin.Begin);
                    writer.Write(blockBinary);
                } 
            }
            else{
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Write(size+1);
                    writer.Seek(size*blockSize+36,SeekOrigin.Begin);
                    block.SetZapMass(0,idRecordBook,InChar(lastname,30),InChar(name,20),InChar(patronymic,30),idGroup);
                    byte[] blockBinary=Combine(block.GetZapMass(0));
                    Console.WriteLine(blockBinary.Length);
                    Array.Resize(ref blockBinary,blockSize);
                    writer.Write(blockBinary);
                    if(first==0)
                    {
                        first = size*blockSize+36;
                        end = size*blockSize+36;
                        writer.Seek(idHashed*8+4,SeekOrigin.Begin);
                        writer.Write(first);
                        writer.Seek(idHashed*8+8,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                    else
                    {
                        writer.Seek(end+440,SeekOrigin.Begin);
                        writer.Write(size*blockSize+36);
                        end = size*blockSize+36;
                        writer.Seek(idHashed*8+8,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                }
            }
        }
    }
}