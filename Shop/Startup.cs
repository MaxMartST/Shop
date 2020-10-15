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
        //�������� ������ �� ������ � dbsettings.json
        private IConfigurationRoot _confString;
        public Startup(IHostingEnvironment hostEnv)
        {
            //��������� ������ �� dbsettings.json
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //����������� �������, �������� ������ �������
        public void ConfigureServices(IServiceCollection services)
        {
            //���������� ���� _confString
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            
            //AddTransient - ���������� ����� ����� ��������� � �����, ������� ��������� ������ ���������
            //������ �������� - ��������� � ������� �� ��������
            //������ �������� - ����� ����� ��������� ������ ���������
            //services.AddTransient<IAllCars, MockCars>();//��������� IAllCars ����������� � MockCars
            //services.AddTransient<ICarsCategory, MockCategory>();//��������� ICarsCategory ����������� � MockCategory

            //������ ����� �� ����� ���������� DB ���� ���������� ����������� ��������� 
            //��������� ����������� �� �������, ������� �������� � DB
            services.AddTransient<IAllCars, CarRepository>();//��������� IAllCars ����������� � MockCars
            services.AddTransient<ICarsCategory, CategoryRepository>();//��������� ICarsCategory ����������� � MockCategory
            services.AddTransient<IAllOrders, OrdersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //������ ������� ��������� ������� ��� ���� ������ �������������
            services.AddScoped(sp => ShopCart.getCart(sp)); 

            //services.AddMvc();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            //��������, ��� ���������� ���� � ������
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();//���������� ��������� �� �������
            app.UseStatusCodePages();//���������� ��� ������
            app.UseStaticFiles();//�������� � ������������ �������
            app.UseSession();//���������� ������
            //app.UseMvcWithDefaultRoute();//����������� url �������� � ���� �� ������ ���������� � ��� (html ���������), �� ����� ���. url �� ���������

            //���� ����������� url ������
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=index}/{id?}");
                routes.MapRoute(name: "categoryFilter", template: "Car/{action}/{category?}", defaults: new { Controller = "Aar", action = "List" });
            });

            //����������� � AppDBContent - ��
            using (var scope = app.ApplicationServices.CreateScope()) 
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                //��� ������ ��������� ���������
                DBObjects.Initial(content);
            };
        }
    }
}
