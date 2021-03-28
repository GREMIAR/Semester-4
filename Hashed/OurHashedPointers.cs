using System;
using System.IO;
namespace Hashed{
    partial class OurBlock{
        
        public void MovingPointers(int start,string filename)
        {
            /*Console.WriteLine("Mid");
            Console.WriteLine(Mid.idZ);
            Console.WriteLine(Mid.addrMain/444);
            Console.WriteLine(Mid.addrbackMain/444);
            Console.WriteLine(Mid.nextB/444);
            Console.WriteLine(Mid.first);
            Console.WriteLine(Mid.end);*/
            int idHashed=Mid.idZ%4;
            if(Mid.addrMain==start)
            {
                //Console.WriteLine("Start_m");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idHashed*8+4+440,SeekOrigin.Begin);
                    writer.Write(Mid.nextB);
                }
            }
            if(Mid.nextB==0)
            {
                //Console.WriteLine("END_m");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idHashed*8+8,SeekOrigin.Begin);
                    writer.Write(Mid.addrbackMain);
                    writer.Seek(Mid.addrbackMain+440,SeekOrigin.Begin);
                    writer.Write(Mid.nextB);
                }
            }
            if(Mid.first==false&&Mid.end==false)
            {
                //Console.WriteLine("SEREDINA_m");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Mid.addrbackMain+440,SeekOrigin.Begin);
                    writer.Write(Mid.nextB);
                }
            }
        }

        public void MovingPointers1(string filename)//addr тот который удаляем
        {
            /*Console.WriteLine("Back");
            Console.WriteLine(Back.idZ);
            Console.WriteLine(Back.addrMain/444);
            Console.WriteLine(Back.addrbackMain/444);
            Console.WriteLine(Back.nextB/444);
            Console.WriteLine(Back.first);
            Console.WriteLine(Back.end);
            Console.WriteLine();*/
            int idHashed=Back.idZ%4;
            if(Back.first==true)
            {
                //Console.WriteLine("Start_b");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idHashed*8+4,SeekOrigin.Begin);
                    writer.Write(Back.addrMain);
                }
            }
            if(Back.end==true)
            {
                //Console.WriteLine("END_b");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(idHashed*8+8,SeekOrigin.Begin);
                    writer.Write(Back.addrbackMain);
                    writer.Seek(Back.addrbackMain+440,SeekOrigin.Begin);
                    writer.Write(Mid.addrMain);
                }
            }
            if(Back.first==false&&Back.end==false)
            {
                //Console.WriteLine("SEREDINA_b");
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek(Back.addrbackMain+440,SeekOrigin.Begin);
                    writer.Write(Mid.addrMain);
                }
            }
        }

        public void Debuging(string filename)
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            for(int i=0;i<4;i++)
            {
                int end = ReadEndBlock(filename,i);
                int first = ReadFirstBlock(filename,i);
                Console.WriteLine("Первый №{0} = {1}", i,first/444 );
                Console.WriteLine("Последий №{0} = {1}", i,end/444);
            }
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            int numBlock = ReadNullBlock(filename);
            Console.WriteLine("Всего блоков = "+ numBlock);
            using (var reader = File.Open(filename, FileMode.Open))
            {
                byte[] blockBinary = new byte[blockSize];
                for(int i=0;i<numBlock;i++)
                {
                    reader.Seek(i*blockSize+36, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, blockSize);
                    ByteArrToBlock(blockBinary);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("Номер блока = "+i);
                    Console.WriteLine("Cсылка на блок = "+block.Nextb/444);
                    PrintBlock();
                }
                reader.Close();
            }
        }
    }
}