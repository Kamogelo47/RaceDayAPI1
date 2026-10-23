
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. ADD CONTROLLERS
// ==========================================

builder.Services.AddControllers();

// ==========================================
// 2. CONFIGURE SQL SERVER DATABASE
// ==========================================

builder.Services.AddDbContext<RaceDayDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "RaceDayConnection"
        )
    )
);

// ==========================================
// 3. CONFIGURE SWAGGER
// ==========================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================================
// 4. REGISTER PASSWORD SERVICE
// ==========================================

builder.Services.AddScoped<PasswordService>();

// ==========================================
// 5. CONFIGURE SESSION SERVICES
// ==========================================

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;

    options.Cookie.SecurePolicy =
        CookieSecurePolicy.Always;

    options.Cookie.SameSite =
        SameSiteMode.Lax;
});

// ==========================================
// 6. BUILD APPLICATION
// ==========================================

var app = builder.Build();

// ==========================================
// 7. CONFIGURE SWAGGER
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ==========================================
// 8. CONFIGURE HTTP PIPELINE
// ==========================================

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// ==========================================
// 9. MAP API CONTROLLERS
// ==========================================

app.MapControllers();

// ==========================================
// 10. RUN APPLICATION
// ==========================================

app.Run();
