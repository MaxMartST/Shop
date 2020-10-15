using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Interfaces
{
    public interface IAllCars
    {
        //возвращает все товары
        //IEnumerable<Car> Cars { get; set; }
        IEnumerable<Car> Cars { get; }
        //возвращает лишь избранные товары
        //IEnumerable<Car> getFavCars { get; set; }
        IEnumerable<Car> getFavCars { get; }
        //возвращать конкретный товар по id
        Car getObjectCar(int carId);
    }
}
