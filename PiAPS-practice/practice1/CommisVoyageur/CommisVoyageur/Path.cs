using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommisVoyageur
{
    class Path
    {
        int point1;
        int point2;
        int length;
        public Path(int point1, int point2, int length)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.length = length;
        }
        public Path(Path path)
        {
            this.point1 = path.point1;
            this.point2 = path.point2;
            this.length = path.length;
        }
        public int GetPoint1()
        {
            return point1;
        }
        public int GetPoint2()
        {
            return point2;
        }
        public int GetLength()
        {
            return length;
        }
        public Path() { }
        public void SetPoint1(int point1)
        {
            this.point1 = point1;
        }
        public void SetPoint2(int point2)
        {
            this.point2 = point2;
        }
        public void SetLength(int length)
        {
            this.length = length;
        }
    }
}
