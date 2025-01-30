namespace EndPointAPI.Models
{
    public class TCKNResponse
    {
        public string StatusMessage { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public long TCKN { get; set; }
    }
}
