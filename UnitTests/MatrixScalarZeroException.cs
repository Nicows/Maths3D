namespace UnitTests;

public class MatrixScalarZeroException: Exception
{
    public MatrixScalarZeroException()
    {
    }

    public MatrixScalarZeroException(string message)
        : base(message)
    {
    }

    public MatrixScalarZeroException(string message, Exception inner)
        : base(message, inner)
    {
    }
    
}