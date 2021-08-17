using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Models;

namespace SportStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            //підключаємося до локальної бази даних
            services.AddDbContext<ApplicationDbContext>(option=>option.UseSqlServer(
                Configuration["ConnectionStrings:DefaultConnection"]));
            //Коли в конструктор контроллера приймає інтерфейс IProductReposetory буде віддавати екземпляр класа EFProductReposetory
            services.AddTransient<IProductReposetory, EFProductReposetory>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddTransient<IOrderReposetory, EFOrderReposetory>();

            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseFileServer();
            app.UseSession();
            app.UseMvc(route => {
                route.MapRoute(
                    name:null,
                    template:"{category}/Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List"});
                route.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });
                route.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });
                route.MapRoute(
                    name: "default",
                    template: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });
                route.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
                
            SeedData.EnsurePopulate(app);
        }
    }
}
