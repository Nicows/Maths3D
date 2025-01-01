using System.Diagnostics;

namespace UnitTests;

public class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat m1, MatrixFloat m2) Apply(MatrixFloat m1, MatrixFloat m2)
    {
        MatrixFloat augmentedMatrix = MatrixFloat.GenerateAugmentedMatrix(m1, m2);
        Console.WriteLine("GenerateAugmentedMatrix:");
        GlobalSettings.DisplayMatrix(augmentedMatrix);
        int i = 0;
        int j = 0;
        while(i < augmentedMatrix.NbLines && j <= augmentedMatrix.NbColumns)
        {
            
            // Étape C: Trouver la ligne k avec la valeur max dans la colonne j
            int k = i;
            float maxVal = augmentedMatrix[i, j]; 

            for (int r = i + 1; r < augmentedMatrix.NbLines; r++)
            {
                if (augmentedMatrix[r, j] > maxVal)
                {
                    k = r;
                    maxVal = augmentedMatrix[r, j]; // Met à jour la valeur max
                }
            }

            // Étape H: Si toutes les valeurs sont nulles dans la colonne, passer à la colonne suivante
            if (maxVal == 0)
            {
                j++;
                continue;
            }

            // Étape D: Échanger les lignes i et k si k != i
            if (k != i)
            {
                GlobalSettings.DisplayMatrix(augmentedMatrix);
                Console.WriteLine($"swap line {i} and {k}");
                MatrixElementaryOperations.SwapLines(augmentedMatrix, i, k);
            }
            GlobalSettings.DisplayMatrix(augmentedMatrix);
            
            // Étape E. Multiplier la ligne i par 1/M(ij)
            Console.WriteLine("Etape E:");
            float valueToReduce = 1 / m1[i, j];
            Console.WriteLine("Value To Reduce:" + valueToReduce);
            for (int l = 0; l < m1.NbColumns; l++)
            {
                if (augmentedMatrix[i, l] != 0)
                {
                    MatrixElementaryOperations.MultiplyLine(augmentedMatrix, j, valueToReduce);
                }
            }
            
            GlobalSettings.DisplayMatrix(augmentedMatrix);
            // Étape F: Pour chaque ligne r où i!=k. Ajouter -M(rj)xL(i) à L(r)
            Console.WriteLine("Etape F:");
            for (int r = 0; r < augmentedMatrix.NbLines; r++)
            {
                if (r == i)
                    continue;
                
                float multiplier = augmentedMatrix[r, j];
                Console.WriteLine($"r={r} and multiplier={multiplier}");
                
                float scalar = -augmentedMatrix[r, i];
                Console.WriteLine($"Scalar={scalar}");
                MatrixElementaryOperations.AddLineToAnother(augmentedMatrix, scalar, r, i );
                
                
                // Console.WriteLine($"Calcul m1: {m1[r, j]}-{multiplier}x{m1[i, j]}");
                // m1[r, j] -= multiplier * m1[i, j];
                // Console.WriteLine($"Calcul m2:{m2[r, 0]}-{multiplier}x{m2[i, 0]}");
                // m2[r, 0] -= multiplier * m2[i, 0];
                GlobalSettings.DisplayMatrix(augmentedMatrix);
                Console.WriteLine();
                
            }
            

            i++;
            j++;

        }
        Console.WriteLine("Final matrix");
        GlobalSettings.DisplayMatrix(m1, m2);

        return (m1, m2);
    }
    

    public static (MatrixFloat, MatrixFloat) ApplyNew(MatrixFloat m1, MatrixFloat m2)
    {
        MatrixFloat tempMatrix = MatrixFloat.GenerateAugmentedMatrix(m1, m2);
        Console.WriteLine("GenerateAugmentedMatrix:");
        GlobalSettings.DisplayMatrix(tempMatrix);
        
        //colonne par colonne
        for (int j = 0; j < m1.NbColumns; j++)
        {
            //on trouve le max de la colonne et son indice
            float columnMaxi = tempMatrix[j, j];
            int indexColumnMaxi = j;
            for (int i = j + 1; i < tempMatrix.NbLines; i++)
            {
                
                if (MathF.Abs(tempMatrix[i, j]) > MathF.Abs(columnMaxi))
                {
                    indexColumnMaxi = i;
                    columnMaxi = tempMatrix[i, j];
                }
            }
            //si le max n'est pas sur la diagonal, on le met dessus
            if (indexColumnMaxi != j)
            {
                MatrixElementaryOperations.SwapLines(tempMatrix, indexColumnMaxi, j);
                Console.WriteLine("Swap lines:");
                GlobalSettings.DisplayMatrix(tempMatrix);
            }
            
            //on met un 1 sur la diagonale en divisant la ligne associée
            if (tempMatrix[j, j] != 0)
            {
                float scalar = 1 / tempMatrix[j, j];
                MatrixElementaryOperations.MultiplyLine(tempMatrix, j, scalar);
            }
            Console.WriteLine("Multiply line:");
            GlobalSettings.DisplayMatrix(tempMatrix);
            
            //on met des 0 sur les autres lignes
            for (int i = 0; i < tempMatrix.NbLines; i++)
            {
                if (i != j)
                {
                    float scalar = -tempMatrix[i, j];
                    Console.WriteLine($"Scalar={scalar}");
                    MatrixElementaryOperations.AddLineToAnother(tempMatrix, scalar, i, j );
                    GlobalSettings.DisplayMatrix(tempMatrix);
                }
            }
        }
        
        Console.WriteLine($"before split");
        GlobalSettings.DisplayMatrix(tempMatrix);
        (m1, m2) = tempMatrix.Split(m1.NbColumns);
        Console.WriteLine($"after split");
        GlobalSettings.DisplayMatrix(m1, m2);
        //on return les 2 matrices séparées
        return (m1, m2);
    }

}