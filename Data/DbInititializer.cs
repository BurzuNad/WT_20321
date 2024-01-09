using Microsoft.AspNetCore.Identity;
using VT_20321.Models;
using VT_20321.Data;
using VT_20321.Data.Catalog;

namespace VT_20321.Data {
    public static class DbInititializer {
        //Метод Seed(), который наполнит базу начальными данными
        public static async Task Seed(WebApplication app) {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // Получить UserManager и RoleManager из сервисов
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            context.Database.EnsureDeleted();  //удалить после
            context.Database.EnsureCreated(); // создать БД, если она еще не создана
            if (!context.Roles.Any()) {  // проверка наличия ролей
                var roleAdmin = new IdentityRole {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                await roleManager.CreateAsync(roleAdmin);  // создать роль admin
            }
            if (!context.Users.Any()) {  // проверка наличия пользователей                
                var user = new ApplicationUser
                {  // создать пользователя
                    Email = "user@gmail.com",
                    UserName = "user@gmail.com"
                };
                await userManager.CreateAsync(user, "qwe-123");
                var admin = new ApplicationUser
                {  // создать пользователя
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com"
                };
                await userManager.CreateAsync(admin, "qwe-123");
                admin = await userManager.FindByEmailAsync("admin@gmail.com");  // назначить роль admin
                await userManager.AddToRoleAsync(admin, "admin");
            }
            // Инициализация Catalogy
            /*if (context.CatalogCategories.Any()) {*/
                context.CatalogCategories.AddRange(new List<CatalogCategory> {
                new CatalogCategory {CatalogCategoryName="Junior"},
                new CatalogCategory {CatalogCategoryName="Professional"}
            });
                await context.SaveChangesAsync();
            /*}*/
           /* if (!context.CatalogItems.Any()) {*/
                context.CatalogItems.AddRange(new List<CatalogItem> {
                new CatalogItem {Name="HeadBoomJR2022о", Technology="Graphene / Auxetic / графит",
                    Price =355, CatalogCategoryId=1, Image="HeadBoomJR2022.jpg"},
                new CatalogItem {Name="HeadGrapheneJR202", Technology="графит/Graphene",
                    Price =288, CatalogCategoryId=1, Image="HeadGrapheneJR2022.jpg"},
                new CatalogItem {Name="HeadSpeedJR2022", Technology="графит/Graphene",
                    Price =304, CatalogCategoryId=1, Image="HeadSpeedJR2022.jpg"},
                new CatalogItem {Name="HeadExtremeProf2022", Technology="графит/Graphene",
                    Price =590, CatalogCategoryId=2, Image="HeadExtremeProf2022.jpg"}
                });
                await context.SaveChangesAsync();
            /*}*/
        }       
    }
}