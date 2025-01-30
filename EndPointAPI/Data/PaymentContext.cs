using EndPointAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EndPointAPI.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }

        public virtual DbSet<Payment> Payments { get; set; }
    }
}
