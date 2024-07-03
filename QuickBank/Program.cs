using QuickBank.Infrastructure.Shared.DependencyInjection;
using QuickBank.Infrastructure.Identity.DependencyInjection;
using QuickBank.Infrastructure.Persistence.DependencyInjection;
using QuickBank.Core.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceDependency(builder.Configuration);
builder.Services.AddApplicationDependency();
builder.Services.AddSharedDependency(builder.Configuration);
builder.Services.AddIdentityDependency(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();
await app.AddIdentitySeeds();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
