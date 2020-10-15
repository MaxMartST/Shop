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
    public class ShopCartController : Controller
    {
        private IAllCars _carRep;
        private readonly ShopCart _shopCart;

        public ShopCartController(IAllCars carRep, ShopCart shopCart)
        {
            _carRep = carRep;
            _shopCart = shopCart;
        }

        //позволить вызвать необходимы йшаблон и отобразить карзину
        public ViewResult Index()
        {
            var items = _shopCart.getShopItems();
            _shopCart.listShopItems = items;

            var obj = new ShopCartViewModel { shopCart = _shopCart };

            return View(obj);
        }

        //функция переадресовывает на другую страницу
        public RedirectToActionResult addToCart(int id)
        {
            //будем выбирать нужный товар из БД по id
            var item = _carRep.Cars.FirstOrDefault(i => i.id == id);

            if (item != null)
            {
                _shopCart.AddToCart(item);
            }

            //после добавления переадресуем пользователя в карзину
            return RedirectToAction("Index");
        }
    }
}
