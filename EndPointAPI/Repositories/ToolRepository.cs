using EndPointAPI.Models;
using System.Diagnostics.Eventing.Reader;

namespace EndPointAPI.Repositories
{
    public interface IToolRepository
    {
        Task<TCKNResponse> CheckTCKNAsync(string tckn);
    }

    public class ToolRepository : IToolRepository
    {
        public async Task<TCKNResponse> CheckTCKNAsync(string tckn)
        {
            var response = new TCKNResponse();
            bool donustu_mu = long.TryParse(tckn, out long tckn_num);
            if (donustu_mu)
            {
                response.TCKN = tckn_num;
                if (tckn_num.ToString().Length == 11)
                {
                    long ilk9 = tckn_num / 100;
                    long son2 = tckn_num % 100;
                    long tekler = 0, ciftler = 0;
                    for (int i = 1; i <= 9; i++, ilk9 /= 10)
                    {
                        long b = ilk9 % 10;
                        if (i % 2 == 0)
                        {
                            ciftler += b;
                        }
                        else
                        {
                            tekler += b;
                        }
                    }
                    long b10 = (tekler * 7 - ciftler) % 10;
                    long b11 = (tekler + ciftler + b10) % 10;
                    if (son2 == b10 * 10 + b11)
                    {
                        response.Status = true;
                        response.StatusMessage = "Kimlik numarası geçerlidir.";
                    }
                    else
                    {
                        response.StatusMessage = "Kimlik numarası geçersizdir.";
                    }
                }
                else
                {
                    response.StatusMessage = "Kimlik no 11 basamaktan oluşmalıdır.";
                }
            }
            else
            {
                response.StatusMessage = "Kimlik no rakamlardan oluşmalıdır.";
            }
            return await Task.Run(() => response);
        }
    }
}
