using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PriceCalculator.App;
using PriceCalculator.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<ScopeService>();
builder.Services.AddScoped<ResourceService>();
builder.Services.AddScoped<DiscountService>();
builder.Services.AddScoped<MSPTierService>();
builder.Services.AddScoped<PriceService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
