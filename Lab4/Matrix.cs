using System;
namespace Lab4{
    public class Matrix {
        int[,] matrix;
        public int Cell(int column,int row)
        {
            return matrix[column,row];
        }
        public void SetCell(int column,int row,int data)
        {
            matrix[column,row]=data;
        }
        int size;
        public int Size {get => this.size;}
        public Matrix(int size) 
        {
            this.size = size;
            matrix = new int[size, size];
        }
        public Matrix()
        {
            size=0;
            matrix = new int[0,0];
        }
        public int[] SearchMin()
        {
            int min=matrix[0,0];
            int column=0 , row=0;
            for(int i=0;i<size;i++)
            {
                for(int f=0;f<size;f++)
                {
                    if(min>matrix[i,f])
                    {
                        column = i;
                        row =f;
                        min = matrix[i,f];
                    }
                }
            }
            return RedOrDire(column,row);
        }
        public int[] RedOrDire(int i, int f)
        {
            int[] beginDG = new int[2];
            while(i>0&&f<size)
            {
                i--;
                f++;
            }
            beginDG[0]=i;
            beginDG[1]=f;
            Console.WriteLine("B1"+beginDG[0]);
            Console.WriteLine("B2"+beginDG[1]);
            return beginDG;
        }
        public void Reset()
        {
            int[] min = SearchMin();
            int ii=min[0];
            int ff=min[1];
            for(int i=0;i<size;i++)
            {
                for(int f=0;f<size;f++)
                {
                    if((i>=ii&&f>=ff)&&(i!=ii||f!=ff))
                    {
                        matrix[i,f]=0;
                    }
                }
                ii++;
                ff--;
            }
        }
    }   
}