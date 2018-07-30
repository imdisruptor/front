using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Backend.Models.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    Initializer.Initialize(userManager, roleManager).Wait();
                }
                catch (Exception) { }

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    SampleData.Initialize(context).Wait();
                }
                catch(Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error pri initialization DATABASE Eeeee");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
    //Shit Code. Delete 
    public static class SampleData
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            //Catalog catalog = new Catalog
            //{
            //    Title = "Second Catalog",
            //    ChildCatalogs = new List<Catalog>(),
            //    Messages = new List<Message>()
            //};
            //    context.Catalogs.Add(catalog);
            //context.Messages.Add(new Message
            //{
            //    Catalog = catalog,
            //    CatalogId = catalog.Id,
            //    DateTime = DateTime.Now,
            //    Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.",
            //    Subject = "Lorem ipsum dolor"
            //});
            //context.Catalogs.ToList()[0].ChildCatalogs.Add(context.Catalogs.ToList()[1]);
            //context.Catalogs.ToList()[1].ParentCatalog = context.Catalogs.ToList()[0];
            //context.Catalogs.ToList()[1].ParentCatalogId = context.Catalogs.ToList()[0].Id;
            await context.SaveChangesAsync();
            //if (true)//!context.Messages.Any() && !context.Catalogs.Any())
            //{
            //    Catalog catalog = new Catalog
            //    {
            //        Title = "Second Catalog",
            //        ChildCatalogs = new List<Catalog>(),
            //        Messages = new List<Message>(),
            //        ParentCatalog = context.Catalogs.ToList()[0],
            //        ParentCatalogId = context.Catalogs.ToList()[0].Id

            //    };
            //    context.Catalogs.Add(catalog);
            //    context.Catalogs.ToList()[0].ChildCatalogs
            //        .Add(context.Catalogs.ToList()[1]);
            //    context.Messages.AddRange(new Message
            //    {
            //        Catalog = catalog,
            //        CatalogId = catalog.Id,
            //        DateTime = DateTime.Now,
            //        Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.",
            //        Subject = "Lorem ipsum dolor"

            //    }
            //    );

            //    await context.SaveChangesAsync();
            //}

        }
    }
}
