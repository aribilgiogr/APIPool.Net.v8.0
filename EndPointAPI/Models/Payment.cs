namespace EndPointAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string CardLast4Digits { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } = "Pending";
        public int OrderId { get; set; }
    }

    public class CreditCardPaymentRequestDTO
    {
        public string CardOwner { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpYear { get; set; } = string.Empty;
        public string ExpMonth { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int OrderId { get; set; }
    }
}
