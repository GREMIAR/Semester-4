using System;
using System.IO;
using System.Text;
namespace BDlab1{
    class Zap{//30+30+20+4+4=84
        int idRecordBook;
        char[] lastname;//30
        char[] name;//20
        char[] middlename;//30
        int idGroup;
        public int GetIdRecordBook(){
            return idRecordBook;
        }
        public char[] GetLastname(){
            return lastname;
        }
        public char[] GetName(){
            return name;
        }
        public char[] GetMiddlename(){
            return middlename;
        }
        public int GetIdGroup(){
            return idGroup;
        }
        public Zap(int idRecordBook,char[] lastname,char[] name,char[] middlename,int idGroup){
            this.idRecordBook = idRecordBook;
            this.lastname = lastname;
            this.name = name;
            this.middlename = middlename;
            this.idGroup = idGroup;
        }
    }
    class Block {
        Zap[] zapMass = new Zap[5];
        int size=4;
        public Zap GetZapMass(int i){
            return zapMass[i];
        }
        public void SetSize(int size){
            this.size=size;
        }
        public int GetSize(){
            return size;
        }
        public Block(Zap[] zapMass){
            this.zapMass[0] = zapMass[0];
            this.zapMass[1] = zapMass[1];
            this.zapMass[2] = zapMass[2];
            this.zapMass[3] = zapMass[3];
            this.zapMass[4] = zapMass[4];
        }
        public Block(){}
    }
}