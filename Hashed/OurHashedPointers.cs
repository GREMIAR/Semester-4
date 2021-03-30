using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{
        
        public void MovingPointers(int start,string filename)
        {
            
            /*Console.WriteLine(start);
            Console.WriteLine(Mid.addr);
            Console.WriteLine(Mid.back);
            Console.WriteLine(Mid.next);
            Console.WriteLine(Mid.start);
            Console.WriteLine(Mid.end);*/
            int idRBHashed=HashFunction(Mid.idZ);

            if(Mid.addr==start)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    Console.WriteLine("StartMid");
                    //Console.WriteLine("xnj="+(idRBHashed*8+4+440));
                    //Console.WriteLine("Mid.next="+Mid.next);
                    Console.WriteLine(Mid.addr+440);
                    Console.WriteLine(Back.next);
                    writer.Seek(Mid.addr+440,SeekOrigin.Begin);
                    writer.Write(Back.next);
                }
            }
            if(Mid.next==0)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    Console.WriteLine("EndMid");
                    writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                    nullBlock.SetPointersEnd(idRBHashed,Mid.back);
                    writer.Write(Mid.back);
                    writer.Seek(Mid.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.next);
                }
            }
            if(!Mid.start&&!Mid.end)
            {
                Console.WriteLine("MidMid");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Mid.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.next);
                }
            }
        }

        public void MovingPointers(string filename)//addr тот который удаляем
        {
            Console.WriteLine(Back.addr);
            Console.WriteLine(Back.back);
            Console.WriteLine(Back.next);
            Console.WriteLine(Back.start);
            Console.WriteLine(Back.end);
            int idRBHashed=HashFunction(Back.idZ);
            if(Back.start)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    Console.WriteLine("StartBack");
                    nullBlock.SetPointersStart(idRBHashed,Back.addr);
                    writer.Seek(idRBHashed*8+4,SeekOrigin.Begin);
                    writer.Write(Back.addr);
                }
            }
            if(Back.end)
            {
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    Console.WriteLine("EndBack");
                    nullBlock.SetPointersEnd(idRBHashed,Back.back);
                    writer.Seek(idRBHashed*8+8,SeekOrigin.Begin);
                    writer.Write(Back.back);
                    writer.Seek(Back.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.addr);
                }
            }
            if(!Back.start&&!Back.end)
            {
                Console.WriteLine("MidBack");
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
                int start = nullBlock.GetPointersStart(i);
                int end = nullBlock.GetPointersEnd(i);
                Console.WriteLine("Первый №{0} = {1}", i,start );
                Console.WriteLine("Последий №{0} = {1}", i,end);
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
                    Console.WriteLine("Cсылка на блок = "+block.Nextb);
                    PrintBlock();
                }
                reader.Close();
            }
        }
    }
}