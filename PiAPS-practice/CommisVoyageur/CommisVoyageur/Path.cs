namespace CommisVoyageur
{
    class Path
    {
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
        public Path(int point1, int point2, int length)
        {
            this.pointFirst = point1;
            this.pointSecond = point2;
            this.length = length;
        }
        public Path(Path path)
        {
            this.pointFirst = path.pointFirst;
            this.pointSecond = path.pointSecond;
            this.length = path.length;
        }
    }
}
