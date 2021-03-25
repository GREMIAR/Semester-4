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
                while(first!=0)
                {
                    reader.Seek(first, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);

                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return numZapFound;
                    }
                    first=block.GetNextb;
                }

            }
            return -1;
        }
        public int Search2(int idRecordBook,string filename)
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
                    reader.Seek(first, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);

                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return first;
                    }
                    first=block.GetNextb;
                }

            }
            return -1;
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
                reader.Seek((numBlock)*blockSize+36, SeekOrigin.Begin);
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
                writer.Seek((numBlock)*blockSize+36,SeekOrigin.Begin);
                writer.Write(blockBinary);
            }
            return true;
        }
        public bool Reset(string filename,int oldidRecordBook)
        {
            int addr = Search2(oldidRecordBook,filename);
            int i;
            for(i=0;i<5;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==oldidRecordBook)
                {
                    break;
                }
            }
            block.SetZapMass(i,0,InChar("0",30),InChar("0",20), InChar("0",30),0);
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(addr,SeekOrigin.Begin);
                byte[] blockBinary = new byte[blockSize];
                blockBinary = Combine();
                writer.Write(blockBinary);
            }
            return true;
            /*int numBlock;
            if((numBlock=Search(oldidRecordBook,filename))==-1){
                Console.WriteLine("Номера зачётки {0} нету",oldidRecordBook);
                return false;
            }
            numBlock=(numBlock-numBlock%5)/5;
            byte[] blockBinary = new byte[blockSize];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek((numBlock)*blockSize+36, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, blockSize);
            }
            ByteArrToBlock(blockBinary);
            for(int i=0;i<5;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==oldidRecordBook)
                {
                    block.SetZapMass(i,0,InChar("0",30),InChar("0",20), InChar("0",30),0);
                }
            }
            blockBinary=Combine();
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek((numBlock)*blockSize+36,SeekOrigin.Begin);
                writer.Write(blockBinary);
            }
            return true;*/
        }

        public void Remove(int idRecordBook,string filename)
        {
            int idHashed = HashFunction(idRecordBook);
            int end = ReadEndBlock(filename,idHashed);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                reader.Seek(end, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, blockSize);
                ByteArrToBlock(blockBinary);
            } // the last block is taken

            int i = 0;
            for(;i<5;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==0)
                {
                    break;
                }
            }
            i--;
            Console.WriteLine("i= "+i);
            Zap lastZap = new Zap(block.GetZapMass(i)); // the last record is taken
            Reset(filename, block.GetZapMass(i).GetIdRecordBook()); // the last record is now marked as deleted
            // now it's time to get the block that is going to be changed
            int addr = Search2(idRecordBook,filename); // got the block, time to find the index of the record to be changed
            for(i=0;i<5;i++)
            {
                if(block.GetZapMass(i).GetIdRecordBook()==idRecordBook)
                {
                    break;
                }
            }
            Console.WriteLine("i= "+i);
            block.SetZapMass(i, lastZap); // the block is changed, time to put it back in the file
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                    writer.Seek(addr,SeekOrigin.Begin);
                    byte[] blockBinary = new byte[blockSize];
                    blockBinary = Combine();
                    writer.Write(blockBinary);
            }

            //now it's time to play with the memory and move blocks here and there


            if(i==0) // case in which the record in the block is the last
            {
                // here it is needed to delete the whole block, not only mark a record as deleted
            }
            else
            {

            }
        }

        public void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            int idHashed = HashFunction(idRecordBook);
            int size = ReadNullBlock(filename);
            int first = ReadFirstBlock(filename,idHashed);
            int end = ReadEndBlock(filename,idHashed);
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
