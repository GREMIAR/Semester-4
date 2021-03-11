using System;
using System.IO;
using System.Text;
namespace BDlab1{
class OurBlock{
        Block block = new Block();
        Zap[] zap = new Zap[5];
        public OurBlock(){
        }
        public int Search(int idRecordBook,string filename)
        {
            Block blockArr = new Block();
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                int numZap = reader.ReadInt32();
                Zap[] zapArr = new Zap[5];
                int i=1,f;
                int numZapFound;
                while(numZap>i)
                {
                    for(f=0;f<5;f++)
                    {
                        if(i>=numZap)
                        {
                            break;    
                        }
                        zapArr[f] = new Zap(reader.ReadInt32(),ByteChar(reader,30),ByteChar(reader,20),ByteChar(reader,30),reader.ReadInt32());
                        i++;
                    }
                    blockArr = new Block(zapArr);
                    blockArr.SetSize(f);
                    if((numZapFound=Student(blockArr,idRecordBook,f))!=-1)
                    {
                        reader.Close();
                        return (numZapFound)*88;
                    }
                }
                Console.WriteLine("\nУпс, ничего не удалось найти\n");
                return -1;
            }
        }

        int Student(Block blockNew,int idRecordBook,int numZip){
            for(int i=0;i<numZip;i++)
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
            for(int i = 0; i < Arr.GetSize(); i++){
                Console.WriteLine("Номер записи в блоке: {0}; Номер зачётки: {1}; Фамилия: {2}; Имя: {3}; Отчество: {4}; Номер группы: {5};",i+1,
                Arr.GetZapMass(i).GetIdRecordBook(), new string(Arr.GetZapMass(i).GetLastname()), new string(Arr.GetZapMass(i).GetName()), 
                new string(Arr.GetZapMass(i).GetMiddlename()), Arr.GetZapMass(i).GetIdGroup());
            }
            Console.WriteLine();
        }
        public void Edit(string filename,int oldidRecordBook,int idRecordBook,string lastname,string name, string patronymic,int idGroup)
        {
            int numZap=Search(oldidRecordBook,filename);
            if(numZap==-1)
            {
                return;
            }
            numZap+=4;
            using (BinaryWriter writer=new BinaryWriter(File.Open(filename, FileMode.Open)))
            {
                writer.Seek(numZap,SeekOrigin.Begin);
                writer.Write(idRecordBook);
                writer.Write(InChar(lastname,30));
                writer.Write(InChar(name,20));
                writer.Write(InChar(patronymic,30));
                writer.Write(idGroup); 
                writer.Close();              
            }
            Console.WriteLine("Заменён на: \n");
            Search(idRecordBook,filename);
        }
        public static char[] InChar(string str, int length)
        {
            char[] charArr = new char[length];
            for (int i = 0; i < length&&str.Length > i; i++)
            {
                charArr[i] = str[i];
            }
            return charArr;
        }

    }
}