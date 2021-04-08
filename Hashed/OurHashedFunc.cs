using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{

        const int blockSize = 448;
        const int nullBlockSize=36;
        const int quantityPointersNullBlock=8;
        const int firstPointerSize=4;
        const int pointerSize=4;

        Block block = new Block();

        NullBlock nullBlock = new NullBlock();

        public void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup,int searchEndCheckResult)
        {
            int idRBHashed = HashFunction(idRecordBook);
            int quantityBlock = nullBlock.QuantityBlock;
            int start = nullBlock.GetPointersStart(idRBHashed);
            int end = nullBlock.GetPointersEnd(idRBHashed);
            if(end==0)
            {
                searchEndCheckResult=-1;
            }
            if(searchEndCheckResult==-2){
                byte[] blockBinary = new byte[blockSize];
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
                    writer.Write(quantityBlock+1);
                    nullBlock.SetQuantityBlock(quantityBlock+1);
                    writer.Seek(quantityBlock*blockSize+nullBlockSize,SeekOrigin.Begin);
                    block.SetZapMass(0,idRecordBook,InChar(lastname,30),InChar(name,20),InChar(patronymic,30),idGroup);
                    byte[] blockBinary=Combine(block.GetZapMass(0));
                    Array.Resize(ref blockBinary,blockSize-4);
                    blockBinary=Combine(blockBinary, BitConverter.GetBytes(end));
                    writer.Write(blockBinary);
                    if(start==0)
                    {
                        start = quantityBlock*blockSize+nullBlockSize;
                        end = start;
                        nullBlock.SetPointersStart(idRBHashed,start);
                        nullBlock.SetPointersEnd(idRBHashed,end);
                        writer.Seek(idRBHashed*quantityPointersNullBlock+firstPointerSize,SeekOrigin.Begin);
                        writer.Write(start);
                        writer.Seek(idRBHashed*quantityPointersNullBlock+pointerSize+firstPointerSize,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                    else
                    {
                        writer.Seek(end+440,SeekOrigin.Begin);
                        end = quantityBlock*blockSize+nullBlockSize;
                        writer.Write(end);
                        nullBlock.SetPointersEnd(idRBHashed,end);
                        writer.Seek(idRBHashed*quantityPointersNullBlock+8,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                }
            }
        }

        public void Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup,int searchResult)
        {
          if (HashFunction(idRecordBook)!=HashFunction(oldidRecordBook))
          {
            Remove(oldidRecordBook, filename,searchResult);
            AddOnEnd(filename, idRecordBook, lastname,name, patronymic, idGroup,searchResult);
          }
          else
          {
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
              block.SetZapMass(FindStudent(oldidRecordBook), idRecordBook, InChar(lastname, 30), InChar(name, 20), InChar(patronymic, 30), idGroup);
              writer.Seek(searchResult,SeekOrigin.Begin);
              writer.Write(Combine());
            }
          }
        }

        public void Remove(int idRecordBook,string filename,int addr)
        {
            int idRBHashed = HashFunction(idRecordBook);
            int quantityBlock = nullBlock.QuantityBlock;
            int end = nullBlock.GetPointersEnd(idRBHashed);
            int numStu = FindStudent(idRecordBook);
            int numlast = FindStudent(0);
            if (numStu==0&&numlast==1)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    if (block.Back!=0)
                    {
                        writer.Seek(block.Back+440,SeekOrigin.Begin);
                        writer.Write(0);
                    }
                    else
                    {
                        writer.Seek(idRBHashed*quantityPointersNullBlock+firstPointerSize,SeekOrigin.Begin);
                        nullBlock.SetPointersStart(idRBHashed,0);
                        writer.Write(0);
                    }
                    writer.Seek(idRBHashed*quantityPointersNullBlock+pointerSize+firstPointerSize,SeekOrigin.Begin);
                    writer.Write(block.Back);
                    nullBlock.SetPointersEnd(idRBHashed,block.Back);
                }
                if(end!=(quantityBlock-1)*blockSize+nullBlockSize)
                {
                    byte[] blockBinary = new byte[blockSize];
                    using (var reader = File.Open(filename, FileMode.Open))
                    {
                        reader.Seek((quantityBlock-1)*blockSize+nullBlockSize, SeekOrigin.Begin);
                        reader.Read(blockBinary, 0, blockSize);
                        ByteArrToBlock(blockBinary);
                    }
                    using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                    {
                        writer.Seek(end,SeekOrigin.Begin);
                        writer.Write(blockBinary);
                        if (block.Back!=0)
                        {
                            writer.Seek(block.Back+440,SeekOrigin.Begin);
                            writer.Write(end);
                        }
                        else
                        {
                            writer.Seek(HashFunction((block.GetZapMass(0).IdRecordBook))*quantityPointersNullBlock+firstPointerSize,SeekOrigin.Begin);
                            nullBlock.SetPointersStart(HashFunction(block.GetZapMass(0).IdRecordBook),end);
                            writer.Write(end);
                        }
                        writer.Seek(HashFunction(block.GetZapMass(0).IdRecordBook)*quantityPointersNullBlock+pointerSize+firstPointerSize,SeekOrigin.Begin);
                        writer.Write(end);
                        nullBlock.SetPointersEnd(HashFunction(block.GetZapMass(0).IdRecordBook),end);
                    }
                }
                FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength((quantityBlock-1)*blockSize+nullBlockSize);
                fileStream.Close();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    nullBlock.SetQuantityBlock(quantityBlock-1);
                    writer.Write(quantityBlock-1);
                }
            }
            else if (numlast==1)
            {
                Zap lastZap = new Zap(block.GetZapMass(0));

                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(block.Back+440,SeekOrigin.Begin);
                    writer.Write(0);
                    writer.Seek(idRBHashed*quantityPointersNullBlock+pointerSize+firstPointerSize,SeekOrigin.Begin);
                    writer.Write(block.Back);
                    nullBlock.SetPointersEnd(idRBHashed,block.Back);
                }

                if(end!=(quantityBlock-1)*blockSize+nullBlockSize)
                {
                    byte[] blockBinary = new byte[blockSize];
                    using (var reader = File.Open(filename, FileMode.Open))
                    {
                        reader.Seek((quantityBlock-1)*blockSize+nullBlockSize, SeekOrigin.Begin);
                        reader.Read(blockBinary, 0, blockSize);
                        ByteArrToBlock(blockBinary);
                    }
                    using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                    {
                        writer.Seek(end,SeekOrigin.Begin);
                        writer.Write(blockBinary);
                        if (block.Back!=0)
                        {
                            writer.Seek(block.Back+440,SeekOrigin.Begin);
                            writer.Write(end);
                        }
                        else
                        {
                            writer.Seek(HashFunction((block.GetZapMass(0).IdRecordBook))*quantityPointersNullBlock+firstPointerSize,SeekOrigin.Begin);
                            nullBlock.SetPointersStart(HashFunction(block.GetZapMass(0).IdRecordBook),end);
                            writer.Write(end);
                        }
                        writer.Seek(HashFunction(block.GetZapMass(0).IdRecordBook)*quantityPointersNullBlock+pointerSize+firstPointerSize,SeekOrigin.Begin);
                        writer.Write(end);
                        nullBlock.SetPointersEnd(HashFunction(block.GetZapMass(0).IdRecordBook),end);
                    }
                }
                using (var reader = File.Open(filename, FileMode.Open))
                {
                    byte[] blockBinary = new byte[blockSize];
                    reader.Seek(addr, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                }
                numStu = FindStudent(idRecordBook);
                block.SetZapMass(numStu, lastZap);
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(addr,SeekOrigin.Begin);
                    byte[] blockBinary = Combine();
                    writer.Write(blockBinary);
                }
                FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength((quantityBlock-1)*blockSize+nullBlockSize);
                fileStream.Close();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    nullBlock.SetQuantityBlock(quantityBlock-1);
                    writer.Write(quantityBlock-1);
                }
            }
            else
            {
                if(numlast==-1)
                {
                    numlast=5;
                }
                numlast--;
                Zap lastZap = new Zap(block.GetZapMass(numlast));
                if(numStu==-1)
                {
                    block.SetZapMass(numlast,0,InChar("0",30),InChar("0",20), InChar("0",30),0);
                    using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                    {
                        writer.Seek(end,SeekOrigin.Begin);
                        byte[] blockBinary = Combine();
                        writer.Write(blockBinary);
                    }
                    using (var reader = File.Open(filename, FileMode.Open))
                    {
                        byte[] blockBinary = new byte[blockSize];
                        reader.Seek(addr, SeekOrigin.Begin);
                        reader.Read(blockBinary, 0, blockSize);
                        ByteArrToBlock(blockBinary);
                    }

                    numStu = FindStudent(idRecordBook);
                    block.SetZapMass(numStu, lastZap);
                    using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                    {
                        writer.Seek(addr,SeekOrigin.Begin);
                        byte[] blockBinary = Combine();
                        writer.Write(blockBinary);
                    }
                }
                else
                {
                    block.SetZapMass(numStu, block.GetZapMass(numlast));
                    block.SetZapMass(numlast,0,InChar("0",30),InChar("0",20), InChar("0",30),0);
                    using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                    {
                        writer.Seek(end,SeekOrigin.Begin);
                        byte[] blockBinary = Combine();
                        writer.Write(blockBinary);
                    }
                }
            }
        }

        public int Search(int idRecordBook,string filename)
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
                    start=block.Next;
                }
            }
            return -1;
        }
    }
}
