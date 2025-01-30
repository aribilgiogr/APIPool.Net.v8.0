namespace EndPointAPI.Models
{
    public class Deprem
    {
        public DateTime TarihSaat { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public double Derinlik { get; set; }
        public double Siddet { get; set; }
        public string Yer { get; set; }
    }
}
