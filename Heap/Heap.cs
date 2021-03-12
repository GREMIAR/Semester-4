using System;
using System.IO;
using System.Text;
namespace BDlab1{
    class Zap{
        int idRecordBook;
        char[] lastname;
        char[] name;
        char[] patronymic;
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
            return patronymic;
        }
        public int GetIdGroup(){
            return idGroup;
        }
        public void SetIdRecordBook(int idRecordBook){
            this.idRecordBook=idRecordBook;
        }
        public void SetLastname(char[] lastname){
            this.lastname=lastname;
        }
        public void SetName(char[] name){
            this.name=name;
        }
        public void SetMiddlename(char[] patronymic){
            this.patronymic=patronymic;
        }
        public void SetIdGroup(int idGroup){
            this.idGroup=idGroup;
        }
        public Zap(int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            this.idRecordBook = idRecordBook;
            this.lastname = lastname;
            this.name = name;
            this.patronymic = patronymic;
            this.idGroup = idGroup;
        }
    }
    class Block {
        Zap[] zapMass = new Zap[5];
        int size=5;
        public Zap GetZapMass(int i){
            return zapMass[i];
        }
        public void SetZapMass(int i,int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            zapMass[i] = new Zap(idRecordBook,lastname,name,patronymic,idGroup);
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