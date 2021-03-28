namespace Hashed{
    class Zap{
        int idRecordBook;
        char[] lastname;
        char[] name;
        char[] patronymic;
        int idGroup;
        public int IdRecordBook{get => this.idRecordBook;}
        public char[] Lastname{get => this.lastname;}
        public char[] Name{get => this.name;}
        public char[] Middlename{get => this.patronymic;}
        public int IdGroup{get => this.idGroup;}

        public Zap(int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            this.idRecordBook = idRecordBook;
            this.lastname = lastname;
            this.name = name;
            this.patronymic = patronymic;
            this.idGroup = idGroup;
        }
        public Zap(Zap record){
            this.idRecordBook = record.idRecordBook;
            this.lastname = record.lastname;
            this.name = record.name;
            this.patronymic = record.patronymic;
            this.idGroup = record.idGroup;
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
        int nextb;
        public int Nextb {get => this.nextb;}
        public void SetNextb(int nextb){
            this.nextb=nextb;
        }
        public Zap GetZapMass(int i){
            return zapMass[i];
        }
        public void SetZapMass(int i,int idRecordBook,char[] lastname,char[] name,char[] patronymic,int idGroup){
            zapMass[i] = new Zap(idRecordBook,lastname,name,patronymic,idGroup);
        }
        public void SetZapMass(int i, Zap record)
        {
          zapMass[i] = new Zap(record.IdRecordBook,record.Lastname,record.Name,record.Middlename,record.IdGroup);
        }

        public Block(Zap[] zapMass){
            nextb=0;
            for(int i=0;i<5;i++)
            {
                this.zapMass[i] = zapMass[i];
            }
        }
        public Block(){
            nextb=0;
            for(int i=0;i<5;i++)
            {
                zapMass[i] = new Zap();
            }
        }
    }
}
