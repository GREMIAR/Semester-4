using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{

        const int blockSize = 444;
        Block block = new Block();
        NullBlock nullBlock = new NullBlock();
        BlockAddr Mid = new BlockAddr();
        BlockAddr Back = new BlockAddr();

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
                    writer.Seek(quantityBlock*blockSize+36,SeekOrigin.Begin);
                    block.SetZapMass(0,idRecordBook,InChar(lastname,30),InChar(name,20),InChar(patronymic,30),idGroup);
                    byte[] blockBinary=Combine(block.GetZapMass(0));
                    Array.Resize(ref blockBinary,blockSize);
                    writer.Write(blockBinary);
                    if(start==0)
                    {
                        start = quantityBlock*blockSize+36;
                        end = start;
                        nullBlock.SetPointersStart(idRBHashed,start);
                        nullBlock.SetPointersEnd(idRBHashed,end);
                        writer.Seek(idRBHashed*8+4,SeekOrigin.Begin);
                        writer.Write(start);
                        writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                    else
                    {
                        writer.Seek(end+440,SeekOrigin.Begin);
                        end = quantityBlock*blockSize+36;
                        writer.Write(end);
                        nullBlock.SetPointersEnd(idRBHashed,end);
                        writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                }
            }
        }

        public void Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup,int searchEndCheckResult)
        {
            Remove(oldidRecordBook, filename);
            AddOnEnd(filename, idRecordBook, lastname,name, patronymic, idGroup,searchEndCheckResult);
        }

        public void Remove(int idRecordBook,string filename)
        {
            int idRBHashed = HashFunction(idRecordBook);
            int start = nullBlock.GetPointersEnd(idRBHashed);
            int end = nullBlock.GetPointersEnd(idRBHashed);
            int quantityBlock = nullBlock.QuantityBlock;
            SearchMid(idRecordBook,filename);
            int numdel=FindStudent(idRecordBook);
            if (numdel==0)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    Console.WriteLine(Mid.back+440);
                    
                    int test=Mid.back;
                    if (test==0)
                    {
                        test=36;
                    }
                    writer.Seek(test+440,SeekOrigin.Begin);
                    writer.Write(0);
                    writer.Seek(idRBHashed*8+4+4,SeekOrigin.Begin);

                    writer.Write(Mid.back);
                    nullBlock.SetPointersEnd(idRBHashed,Mid.back);

                }
                byte[] blockBinary = new byte[blockSize];
                using (var reader = File.Open(filename, FileMode.Open))
                {
                    reader.Seek((quantityBlock-1)*blockSize+36, SeekOrigin.Begin);

                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                }

                SearchBack(block.GetZapMass(0).IdRecordBook,filename);
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Mid.addr,SeekOrigin.Begin);
                    
                    writer.Write(blockBinary); 
                    int test=Back.back;
                    if (test==0)
                    {
                        test=36;
                    }
                    writer.Seek(test+440,SeekOrigin.Begin);
                    
                    writer.Write(Mid.addr);
                    
                    writer.Seek(HashFunction(block.GetZapMass(0).IdRecordBook)*8+4+4,SeekOrigin.Begin);
                    
                    writer.Write(Mid.addr);
                    nullBlock.SetPointersEnd(HashFunction(block.GetZapMass(0).IdRecordBook),Mid.addr);
                }
                FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength((quantityBlock-1)*blockSize+36);
                fileStream.Close();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    nullBlock.SetQuantityBlock(quantityBlock-1);
                    writer.Write(quantityBlock-1);
                }

            }
            else
            {
                byte[] blockBinaryLast = new byte[blockSize];
                byte[] blockBinaryDel = new byte[blockSize];
                using (var reader = File.Open(filename, FileMode.Open))
                {
                    reader.Seek(end, SeekOrigin.Begin);
                    reader.Read(blockBinaryLast, 0, blockSize);
                    ByteArrToBlock(blockBinaryLast);
                }
                int numlast=FindStudent(0);
                numlast--;
                if(numlast==-1)
                {
                    Console.WriteLine("WARNING!!!!!");
                    return;
                }
                Zap lastZap = new Zap(block.GetZapMass(numlast));
                SearchMid(idRecordBook,filename);
                numdel=FindStudent(idRecordBook);
                if(numdel==-1)
                {
                    Console.WriteLine("WARNING!!!!!");
                    return;
                }
                block.SetZapMass(numdel, lastZap); 
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Mid.addr,SeekOrigin.Begin);
                    blockBinaryDel = Combine();
                    writer.Write(blockBinaryDel);
                }
                ByteArrToBlock(blockBinaryLast);
                block.SetZapMass(numlast,0,InChar("0",30),InChar("0",20), InChar("0",30),0);
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(end,SeekOrigin.Begin);
                    blockBinaryLast = Combine();
                    writer.Write(blockBinaryLast);
                }
        

            

            if(FindStudent(0)==0)
            {
                start = nullBlock.GetPointersStart(idRBHashed);

                MovingPointers(start,filename);

                byte[] blockBinary = new byte[blockSize];
                /*if(end==(quantityBlock-1)*blockSize+36){
                    blockBinary=blockBinaryDel;
                    ByteArrToBlock(blockBinary);
                }
                else
                {
                    using (var reader = File.Open(filename, FileMode.Open))
                    {
                        reader.Seek((quantityBlock-1)*blockSize+36, SeekOrigin.Begin);
                        reader.Read(blockBinary, 0, blockSize);
                    }
                    ByteArrToBlock(blockBinary);
                }*/
                Console.WriteLine("Back="+block.GetZapMass(0).IdRecordBook);
                SearchBack(block.GetZapMass(0).IdRecordBook,filename);
                if(Back.addr!=0)
                {

                    MovingPointers(filename);

                    using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                    {
                        writer.Seek(Mid.addr,SeekOrigin.Begin);
                        writer.Write(blockBinary);
                    }
                }
                else
                {
                    Console.WriteLine(block.GetZapMass(0).IdRecordBook);
                }
                
                Debuging(filename);

                FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength((quantityBlock-1)*blockSize+36);
                fileStream.Close();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    nullBlock.SetQuantityBlock(quantityBlock-1);
                    writer.Write(quantityBlock-1);
                }

                Mid.Resize();
                Back.Resize();
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
                    start=block.Nextb;
                }
            }
            return -1;
        }
    }
}
