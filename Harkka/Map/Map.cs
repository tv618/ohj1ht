using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;

/// @author Omanimi
/// @version 23.10.2023
/// <summary>
/// 
/// </summary>
public class Map
{
    /// <summary>
    /// 
    /// </summary>
    public static void Main()
    {
        //Polku tiedostoon. Voidaan jatkossa käyttää muuttujaa kun viitataan tiedostoon.
        const string POLKU = @"C:\Users\Theod\RiderProjects\demot\Harkka\kaikkilogit\btc_usd_spot\Bitstamp_BTCUSD_d.csv";
        pathCheck(POLKU);

        
        string[] luetutRivit = File.ReadAllLines(POLKU); // Tämä avaa tiedoston ja käy sen rivit läpi laittaen ne taulukkoon.
        char[] erottimet = { ',' };  // Tähän voi halutessaan lisätä lisää erottimia, jotka jakavat taulukkoa. 
        string[,] moniUlotteinen = new string[luetutRivit.Length, 10]; // Luodaan moniulotteinen taulukko 
        
        jarjestaTaulukot(luetutRivit, moniUlotteinen, erottimet); // Järjestää tiedostosta luetun taulukon 
        // kaksiulotteiseksi taulukoksi. 
        lisaaViikonpaiva(moniUlotteinen);
        testiMetodi(); // Tämä on tässä sen takia, että saadaan debug pysäytettyä ja tarkistettua mitä taulukoissa on. 
    }

        
    public static void pathCheck(string POLKU)
    {
        if (File.Exists(POLKU))
        {
            Console.WriteLine("Tiedosto on olemassa.");
        }
        else
        {
            Console.WriteLine("Tiedostoa ei löytynyt ");
        }
    }
    
    public static void tulostaTaulukkoInt(int[] taulukko)
    {
        for (int i = 0; i < taulukko.Length; i++)
        {
            Console.WriteLine(taulukko[i]);
        }
    }

    public static void tulostaTaulukkoStr(string[] taulukko)
    {
        for (int i = 0; i < taulukko.Length; i++)
        {
            Console.WriteLine(taulukko[i]);
        }
    }
    
    public static void highLowStatsWeekly()
    {
        //lowestfOfTheWeek();
    }
    /// <summary>
    /// lowest of the week. Import multidimensional array and index of which nummber of the week it is. 
    /// </summary>
    /// <param name="taulukko"></param>
    /// <param name="indeksi"></param>
    /*
    public static lowestOfTheWeek(string[,] taulukko, int indeksi)
    {
        
        return 0; 
    }

    public static highestOfTheWeek(string[,] taulukko, int indeksi)
    {
        return 0;
    }
    */
        
    /// <summary>
    /// Tämä metodi lisää viimeiselle taulukon paikalle viikonpäivän stringinä, monday, tuesday etc.
    /// 28.11.2024 on perjantai, tästä myös lähdetään liikenteeseen. 
    /// </summary>
    /// <param name="taulukko"></param>
    public static void lisaaViikonpaiva(string[,] taulukko)
    {
        string lisattavaTeksti = "";
        int j = 5;
        for (int i = taulukko.GetLength(0) - 1; i > 0; i--) 
        {
            switch (j) // valitsin on lauseke jonka arvo ei ole null
            {
                case 1:
                    lisattavaTeksti = "monday";
                    break;
                case 2:
                    lisattavaTeksti = "tuesday";
                    break;
                case 3:
                    lisattavaTeksti = "wednesday";
                    break;
                case 4:
                    lisattavaTeksti = "thursday";
                    break;
                case 5:
                    lisattavaTeksti = "friday";
                    break;
                case 6:
                    lisattavaTeksti = "saturday";
                    break;
                case 7:
                    lisattavaTeksti = "sunday";
                    j = 0;  //Tämä nollaksi, sitä lisätään heti perään +1, jolloin ensimmäine kytkin on maanantai
                    break;
            }
            taulukko[i, 9] += lisattavaTeksti;
            j++;
        }
    }
    public static void testiMetodi()
    {
        Console.WriteLine("Tultiin testiin");
    }
    /// <summary>
    /// Metodi muuttaa yksiulotteisen taulukon moniulotteiseksi jakaen rivien sisällöt sarakkeisiin
    /// Taulukkoon annetaan string tyyppinen yksiulotteinen taulukko
    /// Taulukon alkiot ovat rivejä kaksiulotteisessa taulukossa
    /// 
    /// </summary>
    /// <param name="taulukko"></param>
    /// <param name="multiTaulukko"></param>
    /// <param name="erottimet"></param>
    public static void jarjestaTaulukot(string[] taulukko, string[,] moniUlotteinen, char[] erottimet)
    {
        string[] parsittu = new string[0];
        //For silmukassa käydään läpi kaikki 
        for (int i = 0; i < taulukko.Length; i++)
        {
            parsittu = taulukko[i].Split(","); // Rivi jaetaan taulukoksi edelleen, erottimena pilkku
            Console.WriteLine();
            for (int j = 0; j < parsittu.Length; j++)
            {
                moniUlotteinen[i, j] = parsittu[j];
            }
        }
    }

}
