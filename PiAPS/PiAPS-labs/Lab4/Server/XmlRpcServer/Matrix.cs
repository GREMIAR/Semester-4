using System;
namespace XmlRpcServer
{
    public class Matrix
    {
        int[,] matrix;
        public int Cell(int column, int row)
        {
            return matrix[column, row];
        }
        public void SetCell(int column, int row, int data)
        {
            matrix[column, row] = data;
        }
        int size;
        public int GetSize()
        {
            return size;
        }
        public void SetSize(int size)
        {
            this.size = size;
            matrix = new int[size, size];
        }
        public Matrix(int size)
        {
            this.size = size;
            matrix = new int[size, size];
        }
        public Matrix()
        {
            size = 0;
            matrix = new int[0, 0];
        }
        public int[] SearchMin()
        {
            int min = matrix[0, 0];
            int column = 0, row = 0;
            for (int i = 0; i < size; i++)
            {
                for (int f = 0; f < size; f++)
                {
                    if (min > matrix[i, f])
                    {
                        column = i;
                        row = f;
                        min = matrix[i, f];
                    }
                }
            }
            return RedOrDire(column, row);
        }
        public int[] RedOrDire(int i, int f)
        {
            int[] beginDG = new int[4];
            while (i < size-1 && f > 0)
            {
                i++;
                f--;
            }
            beginDG[0] = i;
            beginDG[1] = f;
            while (i >= 0 && f < size)
            {
                matrix[i, f] = 0;
                i--;
                f++;
                
            }
            beginDG[2] = i+1;
            beginDG[3] = f-1;
            return beginDG;
        }
        public void Reset()
        {
            int[] min = SearchMin();
            int iDown = min[0];
            int fDown = min[1];
            int iUp = min[2];
            int fUp = min[3];
            for (int i = 0; i < size; i++)
            {
                for (int f = 0; f < size; f++)
                {
                    if (i >= iDown && f >= fDown ||  (i >= iUp && f >= fUp))
                    {
                        matrix[i, f] = (int)Math.Pow(matrix[i, f], 2);
                    }
                }
            }
        }
    }
}
