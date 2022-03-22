using KoalaKit;
using KoalaKit.Caching.Memory;
using KoalaKit.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKoalaKit(builder => builder.AddModules(typeof(KoalaMemoryCacheModule)));
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
