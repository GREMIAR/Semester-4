using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{
        public void Debuging(string filename)
        {
            int idRBHashed = HashFunction(0);
            int quantityBlock = nullBlock.QuantityBlock;
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("Всего блоков = "+ quantityBlock);
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            for(int i=0;i<4;i++)
            {
                int start = nullBlock.GetPointersStart(i);
                int end = nullBlock.GetPointersEnd(i);
                Console.WriteLine("Первый №{0} = {1}", i,start);
                Console.WriteLine("Последий №{0} = {1}", i,end);
            }
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                for(int i=0;i<quantityBlock;i++)
                {
                    reader.Seek(i*blockSize+nullBlockSize, SeekOrigin.Begin);
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