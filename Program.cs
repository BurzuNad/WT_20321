using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VT_20321.Data;
using VT_20321.Data.Catalog;
using VT_20321.Extensions;
using VT_20321.Models;
using VT_20321.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
/*builder.Services.AddDatabaseDeveloperPageExceptionFilter();*/
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;  //подтверждение не нужно
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options =>   //настройка пути к созданным страницам в куки аутентификации
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddSession(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sp => CartService.GetCart(sp)); //зарегистрируйте получение объекта класса Cart из сервисов

builder.Host.ConfigureLogging(logging =>  //фильтр логирования
{
    logging.ClearProviders();
    logging.AddFilter("Microsoft", LogLevel.None);
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}
app.UseHttpsRedirection();
app.UseStaticFiles();  //Возможность работы со статическими файл
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging();

var serviceProvider = app.Services.CreateScope().ServiceProvider;

var logger = serviceProvider.GetRequiredService<ILoggerFactory>();
logger.AddFile("Logs/log-{Date}.txt");

app.UseFileLogging();

//При первом запуске, а затем закомментить
DbInititializer.Seed(app)
               .GetAwaiter()
               .GetResult();

app.Run();
