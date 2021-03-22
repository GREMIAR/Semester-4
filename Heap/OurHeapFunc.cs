using System;
using System.IO;
namespace BDlab1{
    partial class OurBlock{
        //Переделан
        public int Search(int idRecordBook,string filename)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                byte[] blockBinary = new byte[440];
                int numBlock = ReadNullBlockInt(reader);
                int numZapFound;
                for(int i=0;i<numBlock;i++)
                {
                    reader.Read(blockBinary, 0, 440);
                    ByteArrToBlock(blockBinary);
                    if((numZapFound=FindStudent(idRecordBook))!=-1)
                    {
                        reader.Close();
                        return i;
                    }
                }
                reader.Close();
                return -1;
            }
        }

        //Переделан
        public bool Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup)
        {
            int numBlock;
            if((numBlock=Search(oldidRecordBook,filename))==-1){
                Console.WriteLine("Номера зачётки {0} нету",oldidRecordBook);
                return false;
            }
            byte[] blockBinary = new byte[440];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek((numBlock)*440+4, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, 440);
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
                writer.Seek((numBlock)*440+4,SeekOrigin.Begin);
                writer.Write(blockBinary);
            } 
            return true;
        }

        //Не переделан
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
            byte[] blockBinary = new byte[440];
            using (var reader = File.Open(filename, FileMode.Open))
            {
                reader.Seek((numBlock-1)*440+4, SeekOrigin.Begin);
                reader.Read(blockBinary, 0, 440);
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
                fileStream.SetLength((numBlock-1)*440+4);
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
                    writer.Seek((numBlock-1)*440+4,SeekOrigin.Begin);
                    writer.Write(blockBinary);
                }
            }










/*
            if((numBlock=Search(idRecordBook,filename))==-1){
                Console.WriteLine("Номера зачётки {0} нету",idRecordBook);
                return;
            }

            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int numBlock = reader.ReadInt32();
                for(int i=0;i<numBlock;i++)
                {
                    block.SetZapMass(i,reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                }
                reader.Close();
            }*/
            // Удаление последнего блока
                /*FileStream fileStream = new FileStream(filename, FileMode.Open);
                fileStream.SetLength(440);*/
            /*int sizeZap;
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                sizeZap = reader.ReadInt32();
                if(sizeZap==1)
                {
                    Console.WriteLine("Мы ничего не можем удалить, так как БД пуста");
                    return;
                }  
                for(int i=0;i<sizeZap-1;i++)
                {
                    block.SetZapMass(i%5,reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                }
                reader.Close();
            }*/
            /*
            if(Edit(filename,idRecordBook,block.GetZapMass((sizeZap-2)%5).GetIdRecordBook(), InString(block.GetZapMass((sizeZap-2)%5).GetLastname(),30),InString(block.GetZapMass((sizeZap-2)%5).GetName(),20),InString(block.GetZapMass((sizeZap-2)%5).GetMiddlename(),30),block.GetZapMass((sizeZap-2)%5).GetIdGroup())==-1)
            {
                return;
            }*/
           /* using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Write(sizeZap-1);
                writer.Close();
            }*/
        }

        //Переделан
        public void AddOnEnd(string filename, int idRecordBook,string lastname,string name,string patronymic,int idGroup)
        {
            int numBlock = ReadNullBlockInt(filename);
            if(Search(0,filename)!=-1){
                byte[] blockBinary = new byte[440];
                using (var reader = File.Open(filename, FileMode.Open))
                {
                    reader.Seek((numBlock-1)*440+4, SeekOrigin.Begin);
                    reader.Read(blockBinary, 0, 440);
                }
                ByteArrToBlock(blockBinary);
                AddZapOnEnd(idRecordBook,lastname,name,patronymic,idGroup);
                blockBinary=Combine();
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Seek((numBlock-1)*440+4,SeekOrigin.Begin);
                    writer.Write(blockBinary);
                } 
            }
            else{
                using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
                {
                    writer.Write(numBlock+1);
                    writer.Seek(numBlock*440+4,SeekOrigin.Begin);
                    block.SetZapMass(0,idRecordBook,InChar(lastname,30),InChar(name,20),InChar(patronymic,30),idGroup);
                    byte[] blockBinary=Combine(block.GetZapMass(0));
                    Array.Resize(ref blockBinary,440);
                    writer.Write(blockBinary);
                }
            }
        }
    }
}