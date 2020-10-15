using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class AppDBContent : DbContext
    {
        //создадим конструктор класса с парамертами options
        //и передаём параметры options в базлвый конструктор
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        { 
        }

        //получаем и уствнавливаем все товары в магазине
        //DbSet принимает в качестве данных модель 
        //функция получает и устанавливает данные
        public DbSet<Car> Car { get; set; }
        //получаем и уствнавливаем все категории
        public DbSet<Category> Category { get; set; }
        //конкретный товар в карзине
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
    }
}
