namespace UnitTests;

public class MatrixSumException : Exception
{
    public MatrixSumException()
    {
    }

    public MatrixSumException(string message)
        : base(message)
    {
    }

    public MatrixSumException(string message, Exception inner)
        : base(message, inner)
    {
    }
}