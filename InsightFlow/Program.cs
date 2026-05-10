using InsightFlow.Context;
using InsightFlow.Services.AppUsageServices;
using InsightFlow.Services.CategoryUsageServices;
using InsightFlow.Services.KeyInsightServices;
using InsightFlow.Services.OverviewServices;
using InsightFlow.Services.ProductivityServices;
using InsightFlow.Services.RiskUserServices;
using InsightFlow.Services.ScreenTimeProductivityServices;
using InsightFlow.Services.UserTitleUsageServices;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<InsightFlowContext>();

builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppUsageService, AppUsageService>();
builder.Services.AddScoped<ICategoryUsageService, CategoryUsageService>();
builder.Services.AddScoped<IKeyInsightService, KeyInsightService>();
builder.Services.AddScoped<IOverviewService, OverviewService>();
builder.Services.AddScoped<IProductivityService, ProductivityService>();
builder.Services.AddScoped<IRiskUserService, RiskUserService>();
builder.Services.AddScoped<IScreenTimeProductivityService, ScreenTimeProductivityService>();
builder.Services.AddScoped<IUserTitleUsageService, UserTitleUsageService>();


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
