using EndPointAPI.Data;
using EndPointAPI.Models;
using EndPointAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaymentContext>(options => options.UseSqlite("Data source=payments.db"));
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IToolRepository, ToolRepository>();

builder.Services.AddSingleton<DepremRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/tckn_check/{tckn}", async (IToolRepository tool, string tckn) => await tool.CheckTCKNAsync(tckn));

app.MapPost("/api/payment", async (CreditCardPaymentRequestDTO request, IPaymentRepository repository) =>
{
    // Gelen ödeme talebindeki bilgiler banka veritabanýndan kontrol edilir.
    if (request.CardNumber.Length != 16) return Results.BadRequest("Invalid Card Number!");

    var payment = new Payment
    {
        PaymentType = "Credit Card",
        CardLast4Digits = request.CardNumber.Substring(12),
        Amount = request.Amount,
        OrderId = request.OrderId,
        PaymentDate = DateTime.Now,
        Status = "Completed"
    };

    await repository.AddPaymentAsync(payment);
    return Results.Created($"/api/payments/{payment.Id}", payment);
});

app.MapGet("/api/payments", async (IPaymentRepository repository) => Results.Ok(await repository.GetPaymentsAsync()));

app.MapGet("/api/payments/{pay_id}", async (int pay_id, IPaymentRepository repository) =>
{
    var payment = await repository.GetPaymentByIdAsync(pay_id);

    if (payment == null) return Results.NotFound(new { errorMessage = "Payment not found!" });

    return Results.Ok(payment);
});

app.MapGet("/api/sondepremler", async (DepremRepository repo) => await repo.DepremleriGetir());

app.Run();
