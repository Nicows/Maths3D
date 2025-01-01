namespace UnitTests;

public class MatrixInt
{
    private int[,] _array;
    public int this[int i, int j]
    {
        get => _array[i, j]; 
        set => _array[i, j] = value;
    }
    public int NbLines => _array.GetLength(0);
    public int NbColumns => _array.GetLength(1);

    public MatrixInt(int x, int y)
    {
        _array = new int[x, y];
    }
    
    public MatrixInt(int[,] array)
    {
        _array = array;
    }

    public MatrixInt(MatrixInt matrix)
    {
        _array = matrix.ToArray2D().Clone() as int[,] ?? throw new InvalidOperationException();
    }

    public int[,] ToArray2D() => _array;

    public static MatrixInt Identity(int number)
    {
        var identityArray = new int[number, number];
        for (int i = 0; i < number; i++)
        {
            identityArray[i, i] = 1;
        }
        return new MatrixInt(identityArray);
    }

    public bool IsIdentity()
    {
        if(NbLines != NbColumns)
            return false;
        
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                if (i == j && _array[i, j] != 1)
                    return false;
                else if (i != j && _array[i, j] != 0)
                    return false;
                
            }
            
        }
        return true;
    }

    public void Add(MatrixInt matrixToAdd)
    {
        if(!IsSameSize(this, matrixToAdd))
            throw new MatrixSumException();
        
        var arrayToAdd = matrixToAdd.ToArray2D().Clone() as int[,];
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _array[i, j] += arrayToAdd[i, j];
            }
        }
    }
    
    public static MatrixInt Add(MatrixInt matrix1, MatrixInt matrix2)
    {
        if(!IsSameSize(matrix1, matrix2))
            throw new MatrixSumException();
        
        MatrixInt result = new MatrixInt(matrix1.NbLines, matrix1.NbColumns);
        for (int i = 0; i < result.NbLines; i++)
        {
            for (int j = 0; j < result.NbColumns; j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }

        return result;
    }

    public void Multiply(int multiplyBy)
    {
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _array[i, j] *= multiplyBy;
            }
        }
    }
    
    public MatrixInt Multiply(MatrixInt matrixIntMultiply)
    {
        if(!IsSameSize(this, matrixIntMultiply))
            return MultiplyDifferentSize(this, matrixIntMultiply);
            
        MatrixInt currentMatrix = new MatrixInt(NbLines, NbColumns);
        for (int i = 0; i < currentMatrix.NbLines; i++)
        {
            for (int j = 0; j < currentMatrix.NbColumns; j++)
            {
                currentMatrix[i, j] = _array[i, j] * matrixIntMultiply[i, j];
            }
        }
        
        return currentMatrix;
    }

    public static MatrixInt Multiply(MatrixInt matrix, int multiplyBy)
    {
        var currentMatrix = new MatrixInt(matrix.ToArray2D().Clone() as int[,]);
        for (int i = 0; i < currentMatrix.NbLines; i++)
        {
            for (int j = 0; j < currentMatrix.NbColumns; j++)
            {
                currentMatrix[i, j] *= multiplyBy;
            }
        }

        return currentMatrix;
    }
    public static MatrixInt Multiply(MatrixInt matrix, MatrixInt matrix2)
    {
        if (!IsSameSize(matrix, matrix2))
            return MultiplyDifferentSize(matrix, matrix2);
             
        
        var currentMatrix = new MatrixInt(matrix.ToArray2D().Clone() as int[,]);
        for (int i = 0; i < currentMatrix.NbLines; i++)
        {
            for (int j = 0; j < currentMatrix.NbColumns; j++)
            {
                currentMatrix[i, j] = matrix[i, j] * matrix2[i, j];
            }
        }

        return currentMatrix;
    }
    public MatrixInt MultiplyTest(MatrixInt matrix)
    {
        var currentMatrix = new MatrixInt(NbLines,matrix.NbColumns);
        for (int i = 0; i < currentMatrix.NbLines; i++)
        {
            for (int j = 0; j < currentMatrix.NbColumns; j++)
            {
                for (int k = 0; k < NbColumns; k++)
                {
                    currentMatrix[i, j] += _array[i, k] * matrix[k, j];
                }
            }
        }

        return currentMatrix;
    }

    private static MatrixInt MultiplyDifferentSize(MatrixInt matrix, MatrixInt matrix2)
    {
        if(matrix.NbColumns != matrix2.NbLines)
            throw new MatrixMultiplyException();
        
        
        var currentMatrix = new MatrixInt(matrix.NbLines, matrix2.NbColumns);
        for (int i = 0; i < currentMatrix.NbLines; i++)
        {
            for (int j = 0; j < currentMatrix.NbColumns; j++)
            {
                for (int k = 0; k < matrix.NbColumns; k++)
                {
                    currentMatrix[i, j] += matrix[i, k] * matrix2[k, j];
                }
            }
        }

        return currentMatrix;
    }
    
    public static MatrixInt operator +(MatrixInt matrix1, MatrixInt matrix2)
        => Add(matrix1, matrix2);
    
    public static MatrixInt operator -(MatrixInt matrix1, MatrixInt matrix2)
        => Add(matrix1, -matrix2);
    
    public static MatrixInt operator *(MatrixInt matrix, int multiplyBy)
        => Multiply(matrix, multiplyBy);
    
    public static MatrixInt operator *(int multiplyBy, MatrixInt matrix)
        => Multiply(matrix, multiplyBy);
    
    public static MatrixInt operator *(MatrixInt matrix1, MatrixInt matrix2)
        => Multiply(matrix1, matrix2);

    public static MatrixInt operator -(MatrixInt matrix)
    {
        int[,] matrixArray = new int[matrix.NbLines, matrix.NbColumns];
        for (int i = 0; i < matrix.NbLines; i++)
        {
            for (int j = 0; j < matrix.NbColumns; j++)
            {
                matrix[i, j] = -matrix[i, j];
            }
        }

        return matrix;
    }

    static bool IsSameSize(MatrixInt matrix1, MatrixInt matrix2)
    {
        if (matrix1.NbLines != matrix2.NbLines)
            return false;
        else if (matrix1.NbColumns != matrix2.NbColumns)
            return false;

        return true;
    }

    public MatrixInt Transpose()
    {
        return Transpose(this);
    }
    
    public static MatrixInt Transpose(MatrixInt matrix)
    {
        var currentMatrix = new MatrixInt(matrix.NbColumns, matrix.NbLines);
        
        for (int i = 0; i < currentMatrix.NbLines; i++)
        {
            for (int j = 0; j < currentMatrix.NbColumns; j++)
            {
                currentMatrix[i, j] = matrix[j, i];
            }
        }
        return currentMatrix;
    }

    public static MatrixInt GenerateAugmentedMatrix(MatrixInt m1, MatrixInt m2)
    {
        MatrixInt currentMatrix = new MatrixInt(m1.NbLines, m1.NbColumns + m2.NbColumns);

        for (int i = 0; i < currentMatrix.NbLines; i++)
        {
            for (int j = 0; j < currentMatrix.NbColumns; j++)
            {
                if (j < m1.NbColumns)
                    currentMatrix[i, j] = m1[i, j];
                else
                    currentMatrix[i, j] = m2[i, 0];
            }
        }
        return currentMatrix;
    }

    public (MatrixInt m1, MatrixInt m2) Split(int indexToSplit)
    {
        var m1 = new MatrixInt(NbLines, indexToSplit + 1);
        var m2 = new MatrixInt(NbLines, NbColumns - m1.NbColumns);

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < m1.NbColumns; j++)
            {
                m1[i, j] = _array[i, j];
            }
            for (int j = indexToSplit + 1; j < m2.NbColumns; j++)
            {
                m2[i, j] = _array[i, j];
            }
        }
        return (m1, m2);
    }
    
}