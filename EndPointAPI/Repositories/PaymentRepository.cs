using EndPointAPI.Data;
using EndPointAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EndPointAPI.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task AddPaymentAsync(Payment payment);
    }

    public class PaymentRepository:IPaymentRepository
    {
        private readonly PaymentContext context;

        public PaymentRepository(PaymentContext context)
        {
            this.context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await context.Payments.AddAsync(payment);
            await context.SaveChangesAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id) => await context.Payments.FindAsync(id);

        public async Task<IEnumerable<Payment>> GetPaymentsAsync() => await context.Payments.ToListAsync();
    }
}
