using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{
        
        public void MovingPointers(int start,string filename)
        {
            int idRBHashed=HashFunction(Mid.idZ);
            if(Mid.addr==start)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idRBHashed*8+4+440,SeekOrigin.Begin);
                    writer.Write(Mid.next);
                }
            }
            if(Mid.next==0)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                    writer.Write(Mid.back);
                    writer.Seek(Mid.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.next);
                }
            }
            if(!Mid.start&&!Mid.end)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Mid.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.next);
                }
            }
        }

        public void MovingPointers(string filename)//addr тот который удаляем
        {
            int idRBHashed=HashFunction(Back.idZ);
            if(Back.start)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idRBHashed*8+4,SeekOrigin.Begin);
                    writer.Write(Back.addr);
                }
            }
            if(Back.end)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                    writer.Write(Back.back);
                    writer.Seek(Back.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.addr);
                }
            }
            if(!Back.start&&!Back.end)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Back.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.addr);
                }
            }
        }

        public void Debuging(string filename)
        {
            int idRBHashed = HashFunction(0);
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            for(int i=0;i<4;i++)
            {
                int end = nullBlock.GetPointersStart(i);
                int start = nullBlock.GetPointersEnd(i);
                Console.WriteLine("Первый №{0} = {1}", i,start/blockSize );
                Console.WriteLine("Последий №{0} = {1}", i,end/blockSize);
            }
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            int quantityBlock = nullBlock.QuantityBlock;
            Console.WriteLine("Всего блоков = "+ quantityBlock);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                for(int i=0;i<quantityBlock;i++)
                {
                    reader.Seek(i*blockSize+36, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("Номер блока = "+i);
                    Console.WriteLine("Cсылка на блок = "+block.Nextb/blockSize);
                    PrintBlock();
                }
                reader.Close();
            }
        }
    }
}