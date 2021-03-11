using System;
using System.IO;
using System.Text;
namespace BDlab1{
class OurBlock{
        int numBlock;
        int numZap;
        int size;
        Block block = new Block();
        Zap[] zap = new Zap[5];
        public OurBlock(){
            numBlock=-1;
            numZap=-1;
            size=-1;
        }
        public void Search(int idRecordBook,string filename)
        {
            numBlock=0;
            Block blockArr = new Block();
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                Zap[] zapArr = new Zap[5];
                while(reader.PeekChar()!=-1)
                {
                    for(int i=0;i<5;i++)
                    {
                        zapArr[i] = new Zap(reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                        if (reader.PeekChar()==-1)
                        {
                            blockArr = new Block(zapArr);
                            blockArr.SetSize(i);
                            reader.Close();
                            if((numZap=Student(blockArr,idRecordBook))==-1)
                            {
                                Console.WriteLine("\nУпс, ничего не удалось найти\n");
                                return;
                            }
                            return;
                        }
                    }
                    blockArr = new Block(zapArr);
                    if((numZap=Student(blockArr,idRecordBook))!=-1)
                    {
                        reader.Close();
                        return;
                    }
                    numBlock++;

                }
            }
            return;
        }


        
        int Student(Block blockNew,int idRecordBook){
            for(int i=0;i<=blockNew.GetSize();i++)
            {
                if(blockNew.GetZapMass(i).GetIdRecordBook()==idRecordBook){
                    this.block=blockNew;
                    PrintBlock(block);
                    Console.WriteLine("Студент которыго вы искали: Номер зачётки: {0}; Фамилия: {1}; Имя: {2}; Отчество: {3}; Номер группы: {4};\n",
                    blockNew.GetZapMass(i).GetIdRecordBook(), new string(block.GetZapMass(i).GetLastname()), new string(block.GetZapMass(i).GetName()), 
                    new string(block.GetZapMass(i).GetMiddlename()), block.GetZapMass(i).GetIdGroup());
                    return i;
                }
            }
            return -1;
        }
        char[] ByteChar(BinaryReader reader,int length)
        {
            string charArr = "";
            for (int i=0;i<length;i++)
            {
                charArr+=reader.ReadChar();          
                if(charArr[i]=='\0'){
                    for(int f=i+1;f<length;f++)
                    {
                        reader.ReadChar();
                    }
                    break;
                }                 
            }
            return StringinChar(charArr);
        }   
        char[] StringinChar(string str){
            char[] newChar = new char[str.Length-1];
            for (int i=0;i<str.Length-1;i++){
                newChar[i]=str[i];
            }
            return newChar;
        }
        void PrintBlock(Block Arr){
            Console.WriteLine("----Весь Блок----\n");
            for(int i = 0; i <= Arr.GetSize(); i++){
                Console.WriteLine("Номер блока: {0}; Номер зачётки: {1}; Фамилия: {2}; Имя: {3}; Отчество: {4}; Номер группы: {5};",i+1,
                Arr.GetZapMass(i).GetIdRecordBook(), new string(Arr.GetZapMass(i).GetLastname()), new string(Arr.GetZapMass(i).GetName()), 
                new string(Arr.GetZapMass(i).GetMiddlename()), Arr.GetZapMass(i).GetIdGroup());
            }
            Console.WriteLine();
        }
    

    }
}