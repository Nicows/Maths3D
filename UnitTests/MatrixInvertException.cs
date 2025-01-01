namespace UnitTests;

public class MatrixInvertException: Exception
{
    public MatrixInvertException()
    {
    }

    public MatrixInvertException(string message)
        : base(message)
    {
    }

    public MatrixInvertException(string message, Exception inner)
        : base(message, inner)
    {
    }
    
}