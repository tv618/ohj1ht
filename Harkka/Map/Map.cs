using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

/// @author Theodore Veistos
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
        const string POLKU =
            @"C:\Users\Theod\RiderProjects\demot\Harkka\kaikkilogit\btc_usd_spot\Bitstamp_BTCUSD_d.csv";
        pathCheck(POLKU);

        bool fromBegin = true; // True, finds the first monday going from star of the file to end
        // False. find monday from last of the file to beginnign 

        int[,] highsOfTheWeekCount = new int[7,2];
        int[,] lowOfTheWeekCount = new int[7,2];
        float[,] highsOfTheWeekCountFloat = new float[7,2];
        float[,] lowOfTheWeekCountFloat = new float[7,2];
        string[]
            luetutRivit = File.ReadAllLines(POLKU); // Tämä avaa tiedoston ja käy sen rivit läpi laittaen ne taulukkoon.
        char[] erottimet = { ',' }; // Tähän voi halutessaan lisätä lisää erottimia, jotka jakavat taulukkoa. 
        string[,] moniUlotteinen = new string[luetutRivit.Length, 10]; // Luodaan moniulotteinen taulukko 
        /*
        string[,] YearOfFourteen = new string[366, 10];
        string[,] YearOfFifteen = new string[366, 10];
        string[,] YearOfSixteen = new string[366, 10];
        string[,] YearOfSeventeen = new string[366, 10];
        string[,] YearOfEighteen = new string[366, 10];
        string[,] YearOfNineteen = new string[366, 10];
        string[,] YearOfTwenty = new string[366, 10];
        string[,] YearOfTwentyOne = new string[366, 10];
        string[,] YearOfTwentyTwo = new string[366, 10];
        string[,] YearOfTwentyThree = new string[366, 10];
        int[,] allTimeData = new int[7, 3]; // Low, High, Volume
        */
        /*              LOW     HIGH   Day_count
         *  Monday
         *  Tuesday
         *  Wednesday
         *  Thursday
         *  Friday
         *  Saturday
         *  Sunday
         */
        jarjestaTaulukot(luetutRivit, moniUlotteinen, erottimet); // Järjestää tiedostosta luetun taulukon 
        // kaksiulotteiseksi taulukoksi. 
        lisaaViikonpaiva(moniUlotteinen);
        int indexOfMonday = findFirstMonday(moniUlotteinen, fromBegin);
        loseDecimals(moniUlotteinen);
        lowHighPriceActionAllTime(moniUlotteinen, indexOfMonday, fromBegin, highsOfTheWeekCount, lowOfTheWeekCount);
        copyIntToFloat(highsOfTheWeekCount, highsOfTheWeekCountFloat);
        copyIntToFloat(lowOfTheWeekCount, lowOfTheWeekCountFloat);
        makeStats(highsOfTheWeekCountFloat);
        makeStats(lowOfTheWeekCountFloat);
        
        testiMetodi(); // Tämä on tässä sen takia, että saadaan debug pysäytettyä ja tarkistettua mitä taulukoissa on. 
    }

    public static void printStats(float[,] numbers)
    {
        
    }

    public static void copyIntToFloat(int[,] intTable, float[,] floatTable)
    {
        float number;
        for (int i = 0; i < intTable.GetLength(0); i++)
        {
            for (int j = 0; j < intTable.GetLength(1); j++)
            {
                number = intTable[i, j];
                floatTable[i, j] = number;
            }
        }
    }

    public static void makeStats(float[,] numbers)
    {
        float total = 0;
        for (int i = 0; i < numbers.GetLength(0) - 1; i++)
        {
            total += numbers[i, 0];
        }

        for (int i = 0; i < numbers.GetLength(0); i++)
        {
            numbers[i, 1] = numbers[i, 0] / total;
        }
        return;
    }

    
    public static void highLowWeeklyCounter(int smallIndex, int bigIndex, int[,] low, int[,] high)
    {
        switch (smallIndex)
        {
            case 1:
                low[0,0] += 1;
                break;
            case 2:
                low[1,0] += 1;
                break;
            case 3:
                low[2,0] += 1;
                break;
            case 4:
                low[3,0] += 1;
                break;
            case 5:
                low[4,0] += 1;
                break;
            case 6:
                low[5,0] += 1;
                break;
            case 7:
                low[6,0] += 1;
                break;
        }

        switch (bigIndex)
        {
            case 1:
                high[0,0] += 1;
                break;
            case 2:
                high[1,0] += 1;
                break;
            case 3:
                high[2,0] += 1;
                break;
            case 4:
                high[3,0] += 1;
                break;
            case 5:
                high[4,0] += 1;
                break;
            case 6:
                high[5,0] += 1;
                break;
            case 7:
                high[6,0] += 1;
                break;
        }

        return;
    }

    /// <summary>
    /// Removes the decimals after . on lowest and highest of the price on table. 
    /// </summary>
    /// <param name="taulukko"></param>
    public static void loseDecimals(string[,] taulukko)
    {
        String[] splitted;

        for (int i = 3; i < taulukko.GetLength(0) - 1; i++)
        {
            splitted = taulukko[i, 4].Trim().Split('.', StringSplitOptions.None);
            taulukko[i, 4] = splitted[0];
            splitted = taulukko[i, 5].Trim().Split('.', StringSplitOptions.None);
            taulukko[i, 5] = splitted[0];
        }
    }

    public static int numberSwap(int number)
    {
        switch (number)
        {
            case 1:
                number = 7;
                break;
            case 2:
                number = 6;
                break;
            case 3:
                number = 5;
                break;
            case 4:
                number = 4;
                break;
            case 5:
                number = 3;
                break;
            case 6:
                number = 2;
                break;
            case 7:
                number = 1;
                break;
        }

        return number;
    }

    /// <summary>
    /// Basic counter for all time price action for lows and highs.
    /// high is fifth column
    /// low is sixth column
    /// </summary>
    /// <param name="taulukko"></param>
    public static void lowHighPriceActionAllTime(string[,] taulukko, int ind, bool fromStartToEnd, int[,] low,
        int[,] high)
    {
        int j = 1;
        int bigIndex = 1;
        int smallIndex = 1;
        int big = Int32.Parse(taulukko[ind, 4]);
        int small = Int32.Parse(taulukko[ind, 5]);
        int result = 0;

        if (!fromStartToEnd) // FROM END OF THE FILE
        {
            for (int i = ind; i > 4; i--)
            {
                // highest price first 
                result = Convert.ToInt32(taulukko[i, 4]); // asetetaan muuttujaan kyseisen viikonpäivän hinta
                if (result > big) // taulusta haettu on suurempi kuin nykyinen isoin. 
                {
                    big = result; // asetetaan nykyisemmäksi isoksi taulun arvo
                    bigIndex = j;
                }

                result = Convert.ToInt32(taulukko[i, 5]);
                if (result < small)
                {
                    small = result;
                    smallIndex = j;
                }

                if (j == 7)
                {
                    highLowWeeklyCounter(smallIndex, bigIndex, low, high);
                    j = 0;
                    big = Int32.Parse(taulukko[i - 1, 4]); // alustetaan viikon ensimmäsen päivän arvo isoimmaksi 
                    small = Int32.Parse(taulukko[i - 1, 5]); // alustetan viikon ensimmäisen päivän arvo pienimmäksi
                    i--;
                }

                j++;
            }
        }

        else // FROM BEGINNING OF THE FILE
        {
            for (int i = ind + 1; i < taulukko.GetLength(0) - 1; i++) //aloitetaan sunnuntaista. sitten la
            {
                // highest price first 
                result = Convert.ToInt32(taulukko[i, 4]); // asetetaan muuttujaan kyseisen viikonpäivän hinta
                if (result > big) // taulusta haettu on suurempi kuin nykyinen isoin. 
                {
                    big = result; // asetetaan nykyisemmäksi isoksi taulun arvo
                    bigIndex = j;
                }

                result = Convert.ToInt32(taulukko[i, 5]);
                if (result < small)
                {
                    small = result;
                    smallIndex = j;
                }

                if (j == 7)
                {
                    smallIndex = numberSwap(smallIndex);
                    bigIndex = numberSwap(bigIndex);
                    highLowWeeklyCounter(smallIndex, bigIndex, low, high);
                    j = 0;
                    big = Int32.Parse(taulukko[i + 1, 4]); // alustetaan viikon ensimmäsen päivän arvo isoimmaksi 
                    small = Int32.Parse(taulukko[i + 1, 5]); // alustetan viikon ensimmäisen päivän arvo pienimmäksi
                    i++;
                }

                j++;
            }
        }
    }
/*
   /// <summary>
   /// Takes the original table as parameter. Organizes the data to their own tables by year.
   /// </summary>
   /// <param name="taulukko"></param>
   public static void splitToYears(string [,] taulukko)
   {
       int eka = 0;
       int vika = 0;
   }
   public static int weekSplitter(string [,] taulukko, int firstIndex)
   {

       return 0;
   }
   */

        /// <summary>
        /// Finds the data from first monday that is provided on the data
        /// </summary>
        /// <param name="moniUlotteinen"></param>
        /// <returns>index on the location on table on monday. </returns>
        public static int findFirstMonday(string[,] moniUlotteinen, bool fromStartToEnd)
        {
            int i = 0;
            if (fromStartToEnd)
            {
                bool found = false;
                while (!found)
                {
                    if (moniUlotteinen[i, 9] == "monday") found = true;
                    else i++;
                }
            }
            else
            {
                i = moniUlotteinen.GetLength(0) - 1;
                bool found = false;
                while (!found)
                {
                    if (moniUlotteinen[i, 9] == "monday") found = true;
                    else i--;
                }
            }

            return i;
        }

        /// <summary>
        /// Checks if path exists at all and gives feedback on it. This is made for testing the code. 
        /// </summary>
        /// <param name="POLKU"></param>
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
                        j = 0; //Tämä nollaksi, sitä lisätään heti perään +1, jolloin ensimmäine kytkin on maanantai
                        break;
                }

                taulukko[i, 9] += lisattavaTeksti;
                j++;
            }
        }

        /// <summary>
        /// Exists so debugger can be stopped. 
        /// </summary>
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
                parsittu = taulukko[i].Split(erottimet); // Rivi jaetaan taulukoksi edelleen, erottimena pilkku
                Console.WriteLine();
                for (int j = 0; j < parsittu.Length; j++)
                {
                    moniUlotteinen[i, j] = parsittu[j];
                }
            }
        }
    }

    

