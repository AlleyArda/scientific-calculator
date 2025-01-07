using ScientificCalculator.Data;
using ScientificCalculator.DataAccess.Repositories;
using ScientificCalculator.Services.Concrete;
using ScientificCalculator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Postgres connection string
builder.Services.AddDbContext<ScientificCalculatorContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICalculationRepository, CalculationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React uygulamasına izin ver
              .AllowAnyMethod()                     // GET, POST, PUT, DELETE gibi HTTP metodlarına izin ver
              .AllowAnyHeader()                     // Özel başlıkların gönderilmesine izin ver
              .AllowCredentials();                  // Cookie gibi bilgilerin gönderilmesine izin ver
    });
});


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true;                // Güvenlik için sadece HTTP üzerinden erişim
    options.Cookie.IsEssential = true;             // GDPR gibi düzenlemeler için gerekli
    options.Cookie.SameSite = SameSiteMode.None;   // Cross-site kullanımını destekler
    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // HTTPS gereksinimi (lokal için 'None')
});

builder.Services.AddDistributedMemoryCache();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();
app.UseSession(); // Session middleware
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();