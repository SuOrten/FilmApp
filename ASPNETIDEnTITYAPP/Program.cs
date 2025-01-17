using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASPNETIDEnTITYAPP.Areas.Identity.Data;
using ASPNETIDEnTITYAPP.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure database connection string
var connectionString = builder.Configuration.GetConnectionString("DBContextSampleConnection")
    ?? throw new InvalidOperationException("Connection string 'DBContextSampleConnection' not found.");

builder.Services.AddDbContext<DBContextSample>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity to use custom SampleUser and integer keys
builder.Services.AddIdentity<SampleUser, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DBContextSample>()
    .AddDefaultTokenProviders();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient(); // Add HttpClient service here

// Register GenreService and MovieService
builder.Services.AddHttpClient<GenreService>();
builder.Services.AddHttpClient<MovieService>();


// Register the SendGridEmailSender service
var sendGridApiKey = builder.Configuration["SendGrid:ApiKey"];
builder.Services.AddTransient<IEmailSender>(sp => new SendGridEmailSender("")); 





var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();