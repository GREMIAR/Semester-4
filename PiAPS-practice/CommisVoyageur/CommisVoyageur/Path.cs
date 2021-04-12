namespace CommisVoyageur
{
    class Path
    {
        bool direction;//0 - в 1 сторону(от жёлтой к зелёной); 1 - в обе стороны
        public bool Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        int pointFirst;
        public int PointFirst
        {
            get{return pointFirst;}
            set{pointFirst = value;}
        }
        int pointSecond;
        public int PointSecond
        {
            get { return pointSecond; }
            set { pointSecond = value; }
        }
        int length;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public Path() { }
        public Path(int pointFirst, int pointSecond, int length,bool direction)
        {
            this.pointFirst = pointFirst;
            this.pointSecond = pointSecond;
            this.length = length;
            this.direction = direction;
        }
        public Path(Path path)
        {
            this.pointFirst = path.pointFirst;
            this.pointSecond = path.pointSecond;
            this.length = path.length;
        }
    }
}
