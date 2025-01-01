namespace UnitTests;

public class MatrixMultiplyException : Exception
{
    public MatrixMultiplyException()
    {
    }

    public MatrixMultiplyException(string message)
        : base(message)
    {
    }

    public MatrixMultiplyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}