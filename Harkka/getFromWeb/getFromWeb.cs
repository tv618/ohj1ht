using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;


/// @author Omanimi
/// @version 25.10.2023
/// <summary>
/// 
/// </summary>

class getFromWeb
{
    static async Task Main(string[] args)
    {
        // Määritä sivun URL
        string baseUrl = "https://public.bybit.com/trading/BTCUSD/";

        // Latauskansio
        string downloadFolder = @"C:\Users\Theod\RiderProjects\demot\Harkka\kaikkilogit\";

        // Luo HttpClient
        using (HttpClient client = new HttpClient())
        {
            // Tee HTTP-pyyntö sivulle
            string htmlContent = await client.GetStringAsync(baseUrl);

            // Käytä HtmlAgilityPack-kirjastoa parsimaan HTML-sisältö
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Etsi kaikki tiedostolinkit sivulta
            var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]");

            if (links != null)
            {
                foreach (var link in links)
                {
                    string href = link.GetAttributeValue("href", "");
                    if (href.StartsWith("/trading/BTCUSD/"))
                    {
                        string fileUrl = baseUrl + href;
                        string fileName = Path.Combine(downloadFolder, Path.GetFileName(href));

                        // Lataa tiedosto
                        using (var response = await client.GetAsync(fileUrl))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                using (var fileStream = File.Create(fileName))
                                {
                                    await response.Content.CopyToAsync(fileStream);
                                }
                                Console.WriteLine($"Ladattu tiedosto: {fileName}");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Ei löydetty tiedostolinkkejä sivulta.");
            }
        }
    }
}