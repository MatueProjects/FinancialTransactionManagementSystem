using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<FinancialTransactions.Infrastructure.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<FinancialTransactions.Application.Interfaces.IAppDbContext>(sp =>
    sp.GetRequiredService<FinancialTransactions.Infrastructure.Data.AppDbContext>());
builder.Services.AddScoped<FinancialTransactions.Application.Interfaces.ITransactionService, FinancialTransactions.Application.Services.TransactionService>();
builder.Services.AddScoped<FinancialTransactions.Application.Interfaces.ITransactionStatusService, FinancialTransactions.Application.Services.TransactionStatusService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseCors("AngularClient");

app.MapControllers();

app.Run();
