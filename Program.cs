using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using RestfulCrud.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc().AddNewtonsoftJson(x => { x.SerializerSettings.ContractResolver = new DefaultContractResolver(); })
    .AddNewtonsoftJson(x => x.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat)
    .AddNewtonsoftJson(x => x.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local);


var connectionString = builder.Configuration.GetConnectionString("ScheduleDataConnection");
builder.Services.AddDbContext<ScheduleDataContext>(opts => opts.UseSqlServer(connectionString));
builder.Services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

