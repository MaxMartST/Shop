using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data;
using Shop.Data.Interfaces;
using Shop.Data.Mocks;
using Shop.Data.Models;
using Shop.Data.Repository;

namespace Shop
{
    public class Startup
    {
        //Получить данные из строки в dbsettings.json
        private IConfigurationRoot _confString;
        public Startup(IHostingEnvironment hostEnv)
        {
            //получение строки из dbsettings.json
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //регистрация модулей, плагинов внутри проекта
        public void ConfigureServices(IServiceCollection services)
        {
            //подключить файл _confString
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            
            //AddTransient - объединяет между собой интерфейс и класс, который реализует данный интерфейс
            //первый параметр - интерфейс с которым мы работаем
            //второй параметр - какой класс реализует данный интерфейс
            //services.AddTransient<IAllCars, MockCars>();//интерфейс IAllCars реализуется в MockCars
            //services.AddTransient<ICarsCategory, MockCategory>();//интерфейс ICarsCategory реализуется в MockCategory

            //теперь когда мы хотим подключить DB наши интерфейсы реализуются подругому 
            //итерфейсы реализуются от классов, которые связанны с DB
            services.AddTransient<IAllCars, CarRepository>();//интерфейс IAllCars реализуется в MockCars
            services.AddTransient<ICarsCategory, CategoryRepository>();//интерфейс ICarsCategory реализуется в MockCategory
            services.AddTransient<IAllOrders, OrdersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //сервис который разделяет карзину для двух разных пользователей
            services.AddScoped(sp => ShopCart.getCart(sp)); 

            //services.AddMvc();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            //пропишем, что используем кешь и сессии
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();//отображать сообщения об ошибках
            app.UseStatusCodePages();//отображать код ошибок
            app.UseStaticFiles();//работать с статическими файлами
            app.UseSession();//используем сессии
            //app.UseMvcWithDefaultRoute();//отслеживает url страницы и если не указан контроллер и вид (html страничка), то будет исп. url по умолчанию

            //наши собственные url адреса
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=index}/{id?}");
                routes.MapRoute(name: "categoryFilter", template: "Car/{action}/{category?}", defaults: new { Controller = "Aar", action = "List" });
            });

            //подключение к AppDBContent - БД
            using (var scope = app.ApplicationServices.CreateScope()) 
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                //при старте программы вызвается
                DBObjects.Initial(content);
            };
        }
    }
}
