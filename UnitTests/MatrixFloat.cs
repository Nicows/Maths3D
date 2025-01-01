namespace UnitTests;

public class MatrixFloat
{
    private float[,] _arrayFloat;
    public float this[int i, int j]
    {
        get => _arrayFloat[i, j]; 
        set => _arrayFloat[i, j] = value;
    }
    public int NbLines => _arrayFloat.GetLength(0);
    public int NbColumns => _arrayFloat.GetLength(1);

    public MatrixFloat(int x, int y)
    {
        _arrayFloat = new float[x, y];
    }
    
    public MatrixFloat(int size)
    {
        _arrayFloat = new float[size, size];
    }
    
    public MatrixFloat(float[,] arrayFloat)
    {
        _arrayFloat = arrayFloat;
    }

    public MatrixFloat(MatrixFloat matrix)
    {
        _arrayFloat = matrix.ToArray2D().Clone() as float[,] ?? throw new InvalidOperationException();
    }
    
    public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat m1, MatrixFloat m2)
    {
        MatrixFloat currentMatrix = new MatrixFloat(m1.NbLines, m1.NbColumns + m2.NbColumns);

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

    public (MatrixFloat m1, MatrixFloat m2) Split(int indexToSplit)
    {
        var m1 = new MatrixFloat(NbLines, indexToSplit);
        var m2 = new MatrixFloat(NbLines, NbColumns - m1.NbColumns);
        
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < m1.NbColumns; j++)
            {
                m1[i, j] = _arrayFloat[i, j];
            }
            for (int j = 0; j < m2.NbColumns; j++)
            {
                m2[i, j] = _arrayFloat[i, j + indexToSplit];
            }
        }
        return (m1, m2);
    }
    public float[,] ToArray2D() => _arrayFloat;

    
    public MatrixFloat InvertByRowReduction()
    {
        if (NbLines != NbColumns)
            throw new MatrixInvertException("La Matrice doit être carré.");

        MatrixFloat augmentedMatrix = new MatrixFloat(new float[NbLines, NbColumns]);
        for (int i = 0; i < NbLines; i++)
        {
            augmentedMatrix[i, i] = 1f;
        }

        MatrixFloat matrixCopy = new MatrixFloat(ToArray2D().Clone() as float[,]);

        int rowIndex = 0, colIndex = 0;
        while (rowIndex < NbLines && colIndex < NbColumns)
        {
            int maxRow = rowIndex;
            for (int k = rowIndex + 1; k < NbLines; k++)
            {
                if (Math.Abs(matrixCopy[k, colIndex]) > Math.Abs(matrixCopy[maxRow, colIndex]))
                {
                    maxRow = k;
                }
            }

            if (maxRow != rowIndex)
            {
                MatrixElementaryOperations.SwapLines(matrixCopy, rowIndex, maxRow);
                MatrixElementaryOperations.SwapLines(augmentedMatrix, rowIndex, maxRow);
            }

            if (matrixCopy[rowIndex, colIndex] == 0)
            {
                throw new MatrixInvertException("La matrice ne peut pas être inverted.");
            }

            float pivot = matrixCopy[rowIndex, colIndex];
            MatrixElementaryOperations.MultiplyLine(matrixCopy, rowIndex, 1 / pivot);
            MatrixElementaryOperations.MultiplyLine(augmentedMatrix, rowIndex, 1 / pivot);

            for (int r = 0; r < NbLines; r++)
            {
                if (r != rowIndex)
                {
                    float factor = matrixCopy[r, colIndex];
                    MatrixElementaryOperations.AddLineToAnother(matrixCopy, -factor, r, rowIndex);
                    MatrixElementaryOperations.AddLineToAnother(augmentedMatrix, -factor, r, rowIndex);
                }
            }

            rowIndex++;
            colIndex++;
        }

        return augmentedMatrix;
    }

    
    public static MatrixFloat InvertByRowReduction(MatrixFloat matrix)
    {
        return matrix.InvertByRowReduction();
    }
    
    public MatrixFloat SubMatrix(int rowToRemove, int colToRemove)
    {
        int subMatrixRows = NbLines - 1;
        int subMatrixCols = NbColumns - 1;

        MatrixFloat subMatrix = new MatrixFloat(new float[subMatrixRows, subMatrixCols]);
        int subMatrixRow = 0;

        for (int i = 0; i < NbLines; i++)
        {
            if (i == rowToRemove) continue;

            int subMatrixCol = 0;
            for (int j = 0; j < NbColumns; j++)
            {
                if (j == colToRemove) continue;

                subMatrix[subMatrixRow, subMatrixCol] = this[i, j];
                subMatrixCol++;
            }

            subMatrixRow++;
        }

        return subMatrix;
    }
    
    public static MatrixFloat SubMatrix(MatrixFloat matrix, int rowToRemove, int colToRemove)
    {
        int subMatrixRows = matrix.NbLines - 1;
        int subMatrixCols = matrix.NbColumns - 1;

        MatrixFloat subMatrix = new MatrixFloat(new float[subMatrixRows, subMatrixCols]);
        int subMatrixRow = 0;

        for (int i = 0; i < matrix.NbLines; i++)
        {
            if (i == rowToRemove) continue;

            int subMatrixCol = 0;
            for (int j = 0; j < matrix.NbColumns; j++)
            {
                if (j == colToRemove) continue;

                subMatrix[subMatrixRow, subMatrixCol] = matrix[i, j];
                subMatrixCol++;
            }

            subMatrixRow++;
        }

        return subMatrix;
    }
    
    public static MatrixFloat Identity(int size)
    {
        float[,] identityMatrix = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            identityMatrix[i, i] = 1f;
        }
        return new MatrixFloat(identityMatrix);
    }
    
    public MatrixFloat GetSubMatrix(int startRow, int startCol)
    {
        int numRows = NbLines;
        int numCols = NbColumns;

        float[,] subMatrixData = new float[numRows - 1, numCols - 1];

        int subRow = 0;
        for (int row = 0; row < numRows; row++)
        {
            if (row == startRow) continue;  

            int subCol = 0;
            for (int col = 0; col < numCols; col++)
            {
                if (col == startCol) continue;  

                subMatrixData[subRow, subCol] = this[row, col];
                subCol++;
            }
            subRow++;
        }

        return new MatrixFloat(subMatrixData);
    }
    
    public static float Determinant(MatrixFloat matrix)
    {
        int size = matrix.NbLines;

        if (size == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        float determinant = 0;
        for (int col = 0; col < size; col++)
        {
            MatrixFloat subMatrix = matrix.GetSubMatrix(0, col);
            float cofactor = (col % 2 == 0 ? 1 : -1) * matrix[0, col] * Determinant(subMatrix);
            determinant += cofactor;
        }

        return determinant;
    }
    
    public MatrixFloat Adjugate()
    {
        int size = NbLines;
        MatrixFloat adjugateMatrix = new MatrixFloat(size, size);

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                MatrixFloat subMatrix = GetSubMatrix(row, col);
                float cofactor = (float)Math.Pow(-1, row + col) * Determinant(subMatrix);

                adjugateMatrix[col, row] = cofactor;  

            }
        }

        return adjugateMatrix;
    }
    public static MatrixFloat Adjugate(MatrixFloat matrix)
    {
        return matrix.Adjugate();
    }
}