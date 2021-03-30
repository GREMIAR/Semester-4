using System;
namespace Hashed{
    class BlockAddr{
        int addr;
        public int Addr
        {
            get{return addr;}
            set{addr = value;}
        }
        int back;
        public int Back
        {
            get{return back;}
            set{back = value;}
        }
        int next;
        public int Next
        {
            get{return next;}
            set{next = value;}
        }
        int idZ;
        public int IdZ
        {
            get{return idZ;}
            set{idZ = value;}
        }
        public BlockAddr(){}
    }
}