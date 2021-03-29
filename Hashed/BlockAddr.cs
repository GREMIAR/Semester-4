namespace Hashed{
    class BlockAddr{
        public int addr;
        public int back;
        public int next;
        public int idZ;
        public bool end;
        public bool start;
        public void Resize()
        {
            addr=0;
            back=0;
            next=0;
            idZ=0;
            end=false;
            start=false;
        }
        public BlockAddr(int addrMain,int addrbackMain,int nextB,int idZ)
        {
            this.addr=addrMain;
            this.back=addrbackMain;
            this.next=nextB;
            this.idZ=idZ;
        }
        public BlockAddr()
        {
            end=false;
            start=false;
        }
    }
}