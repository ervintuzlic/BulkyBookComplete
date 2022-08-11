//** MVC -> Models Views Controllers
//** Class in C# is used to describe a model component. Model component coresponds with all data. Properties of that class will be columns in table
//** If you have 10 tables in database you will have 10 models that coresponds to them
//** View is all visable part of the website HTML/CSS
//** Button in website and what happens when you click the button, view will interact with model however view can't interact with model directly
//** That's why we have controller which acts like an interface between model and view to process all logic
//** If user uses the button it will send request to Controller which will interact with model (GET DATA) and interact with View (GET PRESENTATION)

//** https:://localhost:55555/Category/Index/3 -> https:://localhost:55555/{Controller}/{Action}/{Id?} Id is optional

//** Models -> Tables in database 

using BulkyBookComplete.DataAccess;
using BulkyBookComplete.DataAccess.Repository;
using BulkyBookComplete.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//** When adding options.UseSqlServer install nuget package sqlserver
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    //GetConnectionString only default ConnectionString in appsettings
    //builder.Configuration.GetConnectionString("DefaultConnection") takes DefaultConnection from appsettings and creates it
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



// Package Console -> add-migration AddCategoryToDatabase creates Migrations folder

//** Add hot reload of the page
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//** Defined files in wwwroot folder
app.UseStaticFiles();

app.UseRouting();

//** Always use authentication before authorization of user. Basic fundamentals of authentication and authorization.
//** Different order in pipeline can break things
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

//** MapControllerRoute that will redirect our request to responding controllers
app.MapControllerRoute(
    name: "default",
    //** Default it would go to Home and search for an action Index
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();