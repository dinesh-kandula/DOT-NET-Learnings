using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrganizationTreeBlazor;
using OrganizationTreeBlazor.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7060/") });
builder.Services.AddScoped<IApiServices, ApiServices>();

builder.Services.AddTelerikBlazor();

await builder.Build().RunAsync();
