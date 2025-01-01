namespace UnitTests;

public class MatrixElementaryOperations
{
    public static void SwapLines(MatrixInt matrix, int line1, int line2)
    {
        int[,] tempArray = matrix.ToArray2D().Clone() as int[,];
        for (int j = 0; j < matrix.NbColumns; j++)
        {
            matrix[line1, j] = tempArray[line2, j];
            matrix[line2, j] = tempArray[line1, j];
        }

    }
    
    public static void SwapLines(MatrixFloat matrix, int line1, int line2)
    {
        float[,] tempArray = matrix.ToArray2D().Clone() as float[,];
        for (int j = 0; j < matrix.NbColumns; j++)
        {
            matrix[line1, j] = tempArray[line2, j];
            matrix[line2, j] = tempArray[line1, j];
        }

    }
    
    public static void SwapColumns(MatrixInt matrix, int column1, int column2)
    {
        int[,] tempArray = matrix.ToArray2D().Clone() as int[,];
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column1] = tempArray[i, column2];
            matrix[i, column2] = tempArray[i, column1];
        }

    }
    public static void SwapColumns(MatrixFloat matrix, int column1, int column2)
    {
        int[,] tempArray = matrix.ToArray2D().Clone() as int[,];
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column1] = tempArray[i, column2];
            matrix[i, column2] = tempArray[i, column1];
        }

    }

    public static void MultiplyLine(MatrixInt matrix, int line, int multiplyBy)
    {
        if (multiplyBy == 0)
            throw new MatrixScalarZeroException();
        
        for (int j = 0; j < matrix.NbColumns; j++)
        {
            matrix[line, j] *= multiplyBy;
        }
    }
    public static void MultiplyLine(MatrixFloat matrix, int line, float multiplyBy)
    {
        if (multiplyBy == 0)
            throw new MatrixScalarZeroException();
        
        for (int j = 0; j < matrix.NbColumns; j++)
        {
            
            matrix[line, j] *= multiplyBy;
        }
    }

    public static void MultiplyColumn(MatrixInt matrix, int column, int multiplyBy)
    {
        if (multiplyBy == 0)
            throw new MatrixScalarZeroException();
        
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column] *= multiplyBy;
        }
    }
    
    public static void MultiplyColumn(MatrixFloat matrix, int column, int multiplyBy)
    {
        if (multiplyBy == 0)
            throw new MatrixScalarZeroException();
        
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column] *= multiplyBy;
        }
    }

    public static void AddLineToAnother(MatrixInt matrix, int increment, int line1, int line2)
    {
        for (int j = 0; j < matrix.NbColumns; j++)
        {
            matrix[line1, j] += matrix[line2, j] + increment++;
        }
    }

    public static void AddLineToAnother(MatrixFloat matrix, float scalar, int line1, int line2)
    {
        for (int j = 0; j < matrix.NbColumns; j++)
        {
            matrix[line1, j] += scalar * matrix[line2, j];
        }
    }

    public static void AddColumnToAnother(MatrixInt matrix, int increment, int column1, int column2)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column1] += matrix[i, column2] + increment++;
        }
    }
    public static void AddColumnToAnother(MatrixFloat matrix, int increment, int column1, int column2)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column1] += matrix[i, column2] + increment++;
        }
    }
}