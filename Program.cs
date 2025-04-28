using Asm6;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Asm6.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Asm6.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Đăng ký dịch vụ xác thực và ủy quyền
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<JwtAuthorizationMessageHandler>();

// Đăng ký các service
builder.Services.AddHttpClient<AccountService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7258/");
});

builder.Services.AddHttpClient<StaffService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7258/"); 
}).AddHttpMessageHandler<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpClient<CustomerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7258/");
}).AddHttpMessageHandler<JwtAuthorizationMessageHandler>();


await builder.Build().RunAsync();
