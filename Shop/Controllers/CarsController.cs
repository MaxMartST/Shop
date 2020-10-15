using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CarsController : Controller
    {
        //контроллер содержит различные функции, при вызове которых у нас будет возвращаться
        //тип данных как ViewResult - тип данных в виде HTML страницы
        private IAllCars _allCars;
        private readonly ICarsCategory _allCategories;

        //создадим конструктор
        public CarsController(IAllCars iAllCars, ICarsCategory iCarsCat)
        {
            _allCars = iAllCars;
            _allCategories = iCarsCat;
        }

        [Route("Cars/List")]
        [Route("Cars/List/{category}")]
        //функция которая возвращает View. Список всех товаров
        public ViewResult List(string category)
        {
            string _category = category;
            //в cars поместим все машины той категории что пришла в функцию
            //параметром category
            IEnumerable<Car> cars = null;
            string currCategory = "";

            //если ничего не передали, то выведем все машины
            if (string.IsNullOrEmpty(category))
            {
                cars = _allCars.Cars.OrderBy(i => i.id);
            }
            else
            {
                //проверим что конкретно находится в category
                if (string.Equals("electro", category, StringComparison.OrdinalIgnoreCase))
                {
                    //выберем автомобили с категорией electro
                    cars = _allCars.Cars.Where(i => i.Category.categoryName.Equals("Электромобили")).OrderBy(i => i.id);
                    currCategory = "Электромобили";
                }

                if (string.Equals("fuel", category, StringComparison.OrdinalIgnoreCase))
                {
                    cars = _allCars.Cars.Where(i => i.Category.categoryName.Equals("Классические автомобили")).OrderBy(i => i.id);
                    currCategory = "Классические автомобили";
                }
            }

            //мы обращаемся к конкретному интерфейсу и через _allCars обращаемся к
            //функции Cars
            //var cars = _allCars.Cars;
            //передаём объект с таварами - машинами
            //затем в View мы обрпбатываем и выводим HTML страницу
            //return View(cars);

            //ещё один способ передать данные в шаблон через ViewBag
            //ViewBag.Category = "My text"
            //var cars = _allCars.Cars;
            //return View(cars);
            //во внутрь шаблона передадуться машины и ViewBag

            var carObj = new CarsListViewModel
            {
                allCars = cars,
                currCategory = currCategory
            };

            //лучший способ экспорта данных в View
             ViewBag.Title = "Страница с автомобилями";
            return View(carObj);
        }
    }
}
