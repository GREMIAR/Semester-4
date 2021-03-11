using System;
using System.IO;
namespace BDlab1{    
    internal class Function  
    {
        public static int ReadNullBlockInt(string filename){  
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                try{  
                    int size = reader.ReadInt32();  
                    reader.Close();
                    return size;  
                }
                catch(IOException e){
                    Console.WriteLine("Exception on reading zero block: " + e);
                }  
                reader.Close();
                return -1;
            }
        }
    }
}