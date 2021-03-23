namespase Lab4{
    public class Matrix {
        double[,] matrix;
    
        int size;
        public int Size {get => this.size;}
        
        public Matrix(int size) 
        {
            this.size = size;
            this.matrix = new double[size, size];
        }
    }   
}