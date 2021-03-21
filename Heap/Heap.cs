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
        public Zap(int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            this.idRecordBook = idRecordBook;
            this.lastname = lastname;
            this.name = name;
            this.patronymic = patronymic;
            this.idGroup = idGroup;
        }
        public Zap()
        {
            idRecordBook=0;
            lastname=new char[0];
            name=new char[0];
            patronymic=new char[0];
            idGroup=0;
        }
    }
    class Block {
        Zap[] zapMass = new Zap[5];
        public Zap GetZapMass(int i){
            return zapMass[i];
        }
        public void SetZapMass(int i,int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            zapMass[i] = new Zap(idRecordBook,lastname,name,patronymic,idGroup);
        }
        public Block(Zap[] zapMass){
            for(int i=0;i<5;i++)
            {
                this.zapMass[i] = zapMass[i];
            }
        }
        public Block(){
            for(int i=0;i<5;i++)
            {
                zapMass[i] = new Zap();
            }
        }
    }
}