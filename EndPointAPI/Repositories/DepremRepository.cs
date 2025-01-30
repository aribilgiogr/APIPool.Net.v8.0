using EndPointAPI.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace EndPointAPI.Repositories
{
    public class DepremRepository
    {
        private readonly HttpClient client;

        public DepremRepository(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }

        public async Task<IEnumerable<Deprem>> DepremleriGetir()
        {
            List<Deprem> result = new List<Deprem>();
            string url = "http://www.koeri.boun.edu.tr/scripts/sondepremler.asp";
            string html = await client.GetStringAsync(url);

            HtmlDocument doc = new();
            doc.LoadHtml(html);

            var preNode = doc.DocumentNode.SelectSingleNode("//pre");

            string data = preNode?.InnerText.Trim();
            if (data != null)
            {
                string[] lines = data.Split("\r\n");
                for (int i = 6; i < 506; i++)
                {
                    string[] cols = Regex.Split(lines[i], @"\s{2,}");
                    var deprem = new Deprem
                    {
                        TarihSaat = DateTime.Parse(cols[0]),
                        Enlem = double.Parse(cols[1]),
                        Boylam = double.Parse(cols[2]),
                        Derinlik = double.Parse(cols[3]),
                        Siddet = double.Parse(cols[5]),
                        Yer = cols[7]
                    };
                    result.Add(deprem);
                }
            }
            return result;
        }
    }
}
