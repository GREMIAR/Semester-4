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
    
    class NullBlock{
        int quantityBlock;
        int zeroStart;
        int zeroEnd;
        int oneStart;
        int oneEnd;
        int twoStart;
        int twoEnd;
        int threeStart;
        int threeEnd;
        public int QuantityBlock => quantityBlock;
        public void SetQuantityBlock(int quantityBlock)
        {
            this.quantityBlock=quantityBlock;
        }
        public int GetPointersStart(int mod)
        {
            int pointers=0;
            if(mod==0)
            {
                pointers = zeroStart;
            }
            else if(mod==1)
            {
                pointers = oneStart;
            }
            else if(mod==2)
            {
                pointers = twoStart;
            }
            else if(mod==3)
            {
                pointers = threeStart;
            }
            return pointers;
        }
        public int GetPointersEnd(int mod)
        {
            int pointers=0;
            if(mod==0)
            {
                pointers = zeroEnd;
            }
            else if(mod==1)
            {
                pointers = oneEnd;
            }
            else if(mod==2)
            {
                pointers = twoEnd;
            }
            else if(mod==3)
            {
                pointers = threeEnd;
            }
            return pointers;
        }
        public void SetPointersStart(int mod,int start)
        {
            if(mod==0)
            {
                zeroStart=start;
            }
            else if(mod==1)
            {
                oneStart=start;
            }
            else if(mod==2)
            {
                twoStart=start;
            }
            else if(mod==3)
            {
                threeStart=start;
            }
        }
        public void SetPointersEnd(int mod,int end)
        {
            if(mod==0)
            {
                zeroEnd=end;
            }
            else if(mod==1)
            {
                oneEnd=end;
            }
            else if(mod==2)
            {
                twoEnd=end;
            }
            else if(mod==3)
            {
                threeEnd=end;
            }
        } 

        public NullBlock(int quantityBlock,int zeroStart,int zeroEnd,int oneStart,int oneEnd,int twoStart,int twoEnd,int threeStart,int threeEnd)
        {
            this.quantityBlock=quantityBlock;
            this.zeroStart=zeroStart;
            this.zeroEnd=zeroEnd;
            this.oneStart=oneStart;
            this.oneEnd=oneEnd;
            this.twoStart=twoStart;
            this.twoEnd=twoEnd;
            this.threeStart=threeStart;
            this.threeEnd=threeEnd;
        }
        public NullBlock(){}
    }
}
