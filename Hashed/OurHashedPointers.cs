using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{
        
        public void MovingPointers(int start,string filename)
        {
            /*Console.WriteLine("Mid");
            Console.WriteLine(Mid.idZ);
            Console.WriteLine(Mid.addrMain/blockSize);
            Console.WriteLine(Mid.addrbackMain/blockSize);
            Console.WriteLine(Mid.nextB/blockSize);
            Console.WriteLine(Mid.start);
            Console.WriteLine(Mid.end);*/
            int idRBHashed=Mid.idZ%4;
            if(Mid.addr==start)
            {
                //Console.WriteLine("Start_m");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idRBHashed*8+4+440,SeekOrigin.Begin);
                    writer.Write(Mid.next);
                }
            }
            if(Mid.next==0)
            {
                //Console.WriteLine("END_m");
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
                //Console.WriteLine("SEREDINA_m");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Mid.back+440,SeekOrigin.Begin);
                    writer.Write(Mid.next);
                }
            }
        }

        public void MovingPointers(string filename)//addr тот который удаляем
        {
            /*Console.WriteLine("Back");
            Console.WriteLine(Back.idZ);
            Console.WriteLine(Back.addrMain/blockSize);
            Console.WriteLine(Back.addrbackMain/blockSize);
            Console.WriteLine(Back.nextB/blockSize);
            Console.WriteLine(Back.start);
            Console.WriteLine(Back.end);
            Console.WriteLine();*/
            int idRBHashed=Back.idZ%4;
            if(Back.start)
            {
                //Console.WriteLine("Start_b");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idRBHashed*8+4,SeekOrigin.Begin);
                    writer.Write(Back.addr);
                }
            }
            if(Back.end)
            {
                //Console.WriteLine("END_b");
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
                //Console.WriteLine("SEREDINA_b");
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