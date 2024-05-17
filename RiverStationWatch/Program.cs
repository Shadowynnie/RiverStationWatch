using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Services;
using RiverStationWatch.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Configure Hangfire to use SQL Server storage
builder.Services.AddHangfire(config => config.UseSqlServerStorage(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Set up Hangfire dashboard
app.UseHangfireDashboard("/Hangfire");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

// Set up Hangfire server
app.UseHangfireServer();

// recurring job to check station records for timeout
RecurringJob.AddOrUpdate<IStationService>("CheckStationForTimeout",
    service => service.CheckStationForTimeout(),
    Cron.MinuteInterval(2)); // Run every 2 minutes for testing
                             //Cron.Hourly);

RecurringJob.AddOrUpdate<IStationService>("CheckFloodLevelExceedance",
    service => service.CheckFloodLevelExceedance(),
    Cron.MinuteInterval(2));
    //Hourly);

app.Run();
