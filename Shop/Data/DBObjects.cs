using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class DBObjects
    {
        //все функции внутри класса DBObjects статические
        //нужно это для того, чтобы мы из других классов обращались 
        //к функциям из этого класса DBObjects

        public static void Initial(AppDBContent content)
        {
            //добавить все категории в лок.БД
            if (!content.Category.Any())
            {
                content.Category.AddRange(Categories.Select(c => c.Value));
            }

            //добавить все неоюходимые объекты товаров
            if (!content.Car.Any())
            {
                //можно добавить как категории или иначе:
                content.AddRange(
                    new Car
                    {
                        name = "Tesla Model S",
                        shortDesc = "Быстрый автомобиль",
                        longDesc = "Красивый, быстрый и очень тихий автомобиль Tesla",
                        img = "/img/tesla-model-s.jpg",
                        price = 45000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Электромобили"]
                    },
                    new Car
                    {
                        name = "Ford Fiesta",
                        shortDesc = "Быстрый автомобиль",
                        longDesc = "Удобный автмобиль для городской жизни",
                        img = "/img/2018-ford-fiesta-st.jpg",
                        price = 11000,
                        isFavourite = false,
                        available = true,
                        Category = Categories["Классические автомобили"]
                    },
                    new Car
                    {
                        name = "BMW M3",
                        shortDesc = "Дерзкий и стильный",
                        longDesc = "Удобный автомобиль для городской жизни",
                        img = "/img/bmw_m3_5.jpg",
                        price = 65000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Классические автомобили"]
                    },
                    new Car
                    {
                        name = "Nissan Leaf",
                        shortDesc = "Бесшумный и экономный",
                        longDesc = "Удобный автомобиль для городской жизни",
                        img = "/img/210-nissan-leaf-2020-ecotechnicacomua-6.jpg",
                        price = 45000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Классические автомобили"]
                    },
                    new Car
                    {
                        name = "Mercedes C class",
                        shortDesc = "Уютный и большой",
                        longDesc = "Удобный автомобиль для городской жизни",
                        img = "/img/asset.MQ6.12.20190705112057.jpeg",
                        price = 14000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Классические автомобили"]
                    }
                );
            }

            //сохранить все изменения в БД
            content.SaveChanges();
        }

        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get 
            {
                if (category == null)
                {
                    var list = new Category[]
                    {
                        new Category { categoryName ="Электромобили", desc = "Современный вид транспорта"},
                        new Category { categoryName = "Классические автомобили", desc = "Машины с двигателем внутреннего згорания"}
                    };

                    category = new Dictionary<string, Category>();
                    foreach (Category elem in list)
                    {
                        category.Add(elem.categoryName, elem);
                    }
                }

                return category;
            }
        }
    }
}
