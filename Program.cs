using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using Microsoft.EntityFrameworkCore;
using React_AggressionFabrications.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddReact();

// A JS engine is registered
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Initialise ReactJS.NET. Must be before static files.
app.UseReact(config =>
{
    // If you want to use server-side rendering of React components,
    // add all the necessary JavaScript files here. This includes
    // your components as well as all of their dependencies.
    // See http://reactjs.net/ for more information. Example:
    //config
    //  .AddScript("~/js/First.jsx")
    //  .AddScript("~/js/Second.jsx");

    // If you use an external build too (for example, Babel, Webpack,
    // Browserify or Gulp), you can improve performance by disabling
    // ReactJS.NET's version of Babel and loading the pre-transpiled
    // scripts. Example:
    //config
    //  .SetLoadBabel(false)
    //  .AddScriptWithoutTransform("~/js/bundle.server.js");
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
