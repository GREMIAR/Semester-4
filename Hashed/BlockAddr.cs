namespace Hashed{
    class BlockAddr{
        public int addrMain;
        public int addrbackMain;
        public int nextB;
        public int idZ;
        public bool end;
        public bool first;
        public BlockAddr(int addrMain,int addrbackMain,int nextB,int idZ)
        {
            this.addrMain=addrMain;
            this.addrbackMain=addrbackMain;
            this.nextB=nextB;
            this.idZ=idZ;
        }
        public void Resize()
        {
            addrMain=0;
            addrbackMain=0;
            nextB=0;
            idZ=0;
            end=false;
            first=false;
        }
        public BlockAddr()
        {
            end=false;
            first=false;
        }
    }
}