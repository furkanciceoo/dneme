using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore; // AddDbContext için
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orbitra.API; // AutoMapperProfile için
using Orbitra.Business.Abstract;
using Orbitra.Business.Concrete;
using Orbitra.Business.Extensions;
using Orbitra.Business.Utilities.Security.Encryption;
using Orbitra.Business.Utilities.Security.JWT;
using Orbitra.DataAccess.Abstract;
using Orbitra.DataAccess.Context;
using Orbitra.DataAccess.EntityFramework;
using Orbitra.DataAccess.Repositories; // VEYA Ef...Dal sýnýflarýnýn namespace'i neyse o
using System.Text.Json.Serialization;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning() // Sadece Uyarý ve Hata (Error) seviyesindekileri yaz
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Serilog'u uygulamanýn loglayýcýsý olarak ayarla
builder.Host.UseSerilog();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrbitraContext>(options =>
    options.UseSqlServer(connectionString, b =>
        b.MigrationsAssembly("Orbitra.DataAccess")));  // <-- EKLENEN KISIM

// 2. DataAccess Katmaný Kayýtlarý (Repository'ler)
// GenericRepository kullandýðýnýz için bu daha da basit olabilir ama
// þimdilik her birini tek tek kaydetmek en net yoldur.
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICityDal, EfCityDal>();
builder.Services.AddScoped<IContentDal, EfContentDal>(); // Bu sizde vardý
builder.Services.AddScoped<ICountryDal, EfCountryDal>();

builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IOperationClaimDal, EfOperationClaimDal>();
builder.Services.AddScoped<IUserOperationClaimDal, EfUserOperationClaimDal>();

// 3. Business Katmaný Kayýtlarý (Manager'lar)
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICityService, CityManager>();
builder.Services.AddScoped<IContentService, ContentManager>(); // Bu sizde vardý
builder.Services.AddScoped<ICountryService, CountryManager>();

// --- GÜVENLÝK SERVÝSLERÝ ---
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<ITokenHelper, JwtHelper>();

// --- JWT DOÐRULAMA AYARLARI ---
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddMemoryCache(); // <-- Önbellekleme servisini aktif eder
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
// 1. Servis Ekleme (Builder kýsmýna)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// --- SWAGGER GÜVENLÝK AYARLARI ---
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orbitra.API", Version = "v1" });

    // "Authorize" butonunu ekle
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });

    // Tüm endpoint'lere kilit simgesini ekle
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    // --- XML DOKÜMANTASYONUNU DAHÝL ET ---
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
