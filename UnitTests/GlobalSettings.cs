namespace UnitTests;

public class GlobalSettings
{
    public static double DefaultFloatingPointTolerance { get; set; }
    
    public static void DisplayMatrix(MatrixFloat m1, MatrixFloat m2)
    {
        for (int i = 0; i < m1.NbLines; i++)
        {
            
            for (int j = 0; j < m1.NbColumns + m2.NbLines; j++)
            {
                if (j < m1.NbColumns)
                {
                    if (j == 0)
                        Console.Write($"[{m1[i, j]} ");
                    else if (j == m1.NbColumns - 1)
                        Console.Write($"{m1[i, j]}]");
                    else
                        Console.Write($"{m1[i, j]} ");
                }
                
                
            }
            Console.WriteLine($"[{m2[i, 0]}]");
        }
        // for (int i = 0; i < m2.NbLines; i++)
        //     Console.WriteLine($"[{m2[i, 0]}]");
        Console.WriteLine();
    }
    
    public static void DisplayMatrix(MatrixFloat m1)
    {
        if (m1.NbColumns == 1)
        {
            for (int i = 0; i < m1.NbLines; i++)
                Console.WriteLine($"[{m1[i, 0]}]");
            return;
        }
        for (int i = 0; i < m1.NbLines; i++)
        {
            
            for (int j = 0; j < m1.NbColumns; j++)
            {
                if (j == 0)
                    Console.Write($"[{m1[i, j]} ");
                else if (j == m1.NbColumns - 1)
                    Console.WriteLine($"{m1[i, j]}]");
                else
                    Console.Write($"{m1[i, j]} ");
                
            }
        }
        Console.WriteLine();
    }
}