using Microsoft.EntityFrameworkCore;
using PriceCalculator.Api.Services;
using PriceCalculator.Data.Data;
using PriceCalculator.Data.Interfaces;
using PriceCalculator.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        , b => b.MigrationsAssembly("PriceCalculator.Data")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IMSPTierRepository, MSPTierRepository>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IScopeRepository, ScopeRepository>();
builder.Services.AddScoped<IDiscountResourceRepository, DiscountResourceRepository>();
builder.Services.AddScoped<PriceCalculationService>();

builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Enable CORS middleware
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
