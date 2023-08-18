using BookTracker.Data;
using BookTracker.Managers;
using Microsoft.EntityFrameworkCore;


public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //lets add services to the container
        builder.Services.AddControllersWithViews();

        //lets add DbCOntext to the services
        builder.Services.AddDbContext<BookTrackerContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        //Added dependency injection injecting manager
        builder.Services.AddSingleton<ICategoryManager, CategoryManager>();
        builder.Services.AddSingleton<IBookManager, BookManager>();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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

        app.Run();
    }
}