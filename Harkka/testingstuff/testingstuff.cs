using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// @author Omanimi
/// @version 13.11.2023
/// <summary>
/// 
/// </summary>
public class testingstuff
{
    /// <summary>
    /// 
    /// </summary>
    public static void Main()
    {

        int luku = 23;
        float luku2 = luku;
        Console.WriteLine(luku2 + " " + (luku2.GetType()));
        Console.WriteLine(luku + " " + luku.GetType());
        luku2.GetTypeCode();

        /*
        string[,] taulukko = new String[3, 2];
        taulukko[0, 0] = "kani";
        taulukko[1, 0] = "pani";
        taulukko[2, 0] = "joukoa";

        taulukko[0, 1] = "384.99";
        taulukko[1, 1] = "10.10";
        taulukko[2, 1] = "9";

        for (int i = 0; i < taulukko.GetLength(0); i++)
        {
            for (int j = 0; j < taulukko.GetLength(1); j++)
            {
                Console.WriteLine(taulukko[i,j]);
            }
        }

        String[] pilkottu;
        for (int i = 0; i < 3; i++)
        {
            pilkottu = taulukko[i, 1].Trim().Split('.', StringSplitOptions.None);
        }

        string luku_stringina = "343.1";
        float luku = float.Parse("233.44");

        Console.WriteLine("A" + luku);
        */
        /*
        try
        {
            double liukulukuna = double.Parse(sana);
            Console.WriteLine("A" + liukulukuna);
        }
        catch (Exception e)
        {
            Console.WriteLine("B" + e);
            throw;
        }
        finally
        {
            Console.WriteLine("C");
        }
        */
    }
}