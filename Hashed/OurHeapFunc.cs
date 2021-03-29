using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{

        const int blockSize = 444;
        Block block = new Block();
        BlockAddr Mid = new BlockAddr();
        BlockAddr Back = new BlockAddr();

        public void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            int idRBHashed = HashFunction(idRecordBook);
            int quantityBlock = ReadNullBlock(filename);
            int first = ReadFirstBlock(filename,idRBHashed);
            int end = ReadEndBlock(filename,idRBHashed);
            if(CheckLastBlock(filename,end)){
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
                    writer.Seek(quantityBlock*blockSize+36,SeekOrigin.Begin);
                    block.SetZapMass(0,idRecordBook,InChar(lastname,30),InChar(name,20),InChar(patronymic,30),idGroup);
                    byte[] blockBinary=Combine(block.GetZapMass(0));
                    Array.Resize(ref blockBinary,blockSize);
                    writer.Write(blockBinary);
                    if(first==0)
                    {
                        first = quantityBlock*blockSize+36;
                        end = quantityBlock*blockSize+36;
                        writer.Seek(idRBHashed*8+4,SeekOrigin.Begin);
                        writer.Write(first);
                        writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                    else
                    {
                        writer.Seek(end+440,SeekOrigin.Begin);
                        writer.Write(quantityBlock*blockSize+36);
                        end = quantityBlock*blockSize+36;
                        writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                        writer.Write(end);
                    }
                }
            }
        }

        public void Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup)
        {
            Remove(oldidRecordBook, filename);
            AddOnEnd(filename, idRecordBook, lastname,name, patronymic, idGroup);
        }

        public void Remove(int idRecordBook,string filename)
        {
            int quantityBlock = ReadNullBlock(filename);
            int idRBHashed = HashFunction(idRecordBook);
            int end = ReadEndBlock(filename,idRBHashed);
            int first = ReadFirstBlock(filename,idRBHashed);
            byte[] blockBinary1;
            using (var reader = File.Open(filename, FileMode.Open))
            {
                blockBinary1 = new byte[blockSize];
                reader.Seek(end, SeekOrigin.Begin);
                reader.Read(blockBinary1, 0, blockSize);
                ByteArrToBlock(blockBinary1);
            }
            
            int i = 0;
            for(;i<5;i++)
            {
                if(block.GetZapMass(i).IdRecordBook==0)
                {
                    break;
                }
            }
            i--;
            
            int num=i;
            Zap lastZap = new Zap(block.GetZapMass(i));

            int addrDelBlock = Search2(idRecordBook,filename);
            for(i=0;i<5;i++)
            {
                if(block.GetZapMass(i).IdRecordBook==idRecordBook)
                {
                    break;
                }
            }
            if(i==5)
            {
                return;
            }
            block.SetZapMass(i, lastZap); 
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(addrDelBlock,SeekOrigin.Begin);
                byte[] blockBinary = new byte[blockSize];
                blockBinary = Combine();
                writer.Write(blockBinary);
            }
            using (var reader = File.Open(filename, FileMode.Open))
            {
                blockBinary1 = new byte[blockSize];
                reader.Seek(end, SeekOrigin.Begin);
                reader.Read(blockBinary1, 0, blockSize);
                ByteArrToBlock(blockBinary1);
            } 
            block.SetZapMass(num,0,InChar("0",30),InChar("0",20), InChar("0",30),0);
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(end,SeekOrigin.Begin);
                byte[] blockBinary = new byte[blockSize];
                blockBinary = Combine();
                writer.Write(blockBinary);
            }
            for(i=0;i<5;i++)
            {
                if(block.GetZapMass(i).IdRecordBook==0)
                {
                    break;
                }
            }
            if(i==0) 
            {
                idRBHashed = HashFunction(idRecordBook);
                end = ReadEndBlock(filename,idRBHashed);
                first = ReadFirstBlock(filename,idRBHashed);
                MovingPointers(first,filename);

                byte[] blockBinary = new byte[blockSize];
                using (var reader = File.Open(filename, FileMode.Open))
                {
                    reader.Seek((quantityBlock-1)*blockSize+36, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                }
                ByteArrToBlock(blockBinary);
                Search3(block.GetZapMass(0).IdRecordBook,filename);

                MovingPointers(filename);

                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(addrDelBlock,SeekOrigin.Begin);
                    writer.Write(blockBinary);
                }
                
                FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength((quantityBlock-1)*blockSize+36);
                fileStream.Close();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Write(quantityBlock-1);
                }
                Mid.Resize();
                Back.Resize();
                
            }
        }

        public int Search(int idRecordBook,string filename)
        {
            int idRBHashed = HashFunction(idRecordBook);
            int end = ReadEndBlock(filename,idRBHashed);
            int first = ReadFirstBlock(filename,idRBHashed);
            int quantityBlock = ReadNullBlock(filename);
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
                    first=block.Nextb;
                }
            }
            return -1;
        }

        public int Search2(int idRecordBook,string filename)
        {
            Mid.idZ=idRecordBook;
            int idRBHashed = HashFunction(idRecordBook);
            int end = ReadEndBlock(filename,idRBHashed);
            int first = ReadFirstBlock(filename,idRBHashed);
            int quantityBlock = ReadNullBlock(filename);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                int numZapFound;
                int backAddr=0;
                int temp=0;
                while(first!=0)
                {   
                    reader.Seek(first, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        Mid.addrbackMain=backAddr;
                        Mid.addrMain=first;
                        Mid.nextB=block.Nextb;
                        if(temp==0)
                        {
                            Mid.first=false;
                        }
                        if(block.Nextb==0)
                        {
                            Mid.end=true;
                        }
                        reader.Close();
                        return first;
                    }
                    backAddr=first;
                    first=block.Nextb;
                    temp+=1;
                }

            }
            return -1;
        }

        public int Search3(int idRecordBook,string filename)
        {
            Back.idZ=idRecordBook;

            int idRBHashed = HashFunction(idRecordBook);
            int end = ReadEndBlock(filename,idRBHashed);
            int first = ReadFirstBlock(filename,idRBHashed);
            int quantityBlock = ReadNullBlock(filename);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                int numZapFound;
                int backAddr=0;
                int temp=0;
                while(first!=0)
                {
                    reader.Seek(first, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    
                    ByteArrToBlock(blockBinary);

                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        Back.addrbackMain=backAddr;
                        Back.addrMain=first;
                        Back.nextB=block.Nextb;
                        if(temp==0)
                        {
                            Back.first=true;
                        }
                        if(block.Nextb==0)
                        {
                            Back.end=true;
                        }
                        reader.Close();
                        return first;
                    }
                    backAddr=first;
                    first=block.Nextb;
                    temp+=1;
                }

            }
            return -1;
        }
    }
}
