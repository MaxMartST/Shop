using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllOrders allOrders;
        private readonly ShopCart shopCart;

        //конструктор
        public OrderController(IAllOrders allOrders, ShopCart shopCart)
        {
            this.allOrders = allOrders;
            this.shopCart = shopCart;
        }

        //функция возвращающая htnl шаблон
        //над этим шаблоном будут проходить действия, заполнение формы
        //событие Опляты заказа. Поэтому исп. интерфейс чтобы принимать данные
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]//отправка методом POST
        public IActionResult Checkout(Order order)
        {
            //берем все товары из карзины и помещаем в список для удобной работы 
            shopCart.listShopItems = shopCart.getShopItems();

            //если при отправке товаров нет, то выдаём ошибку
            if (shopCart.listShopItems.Count == 0)
            {
                //выдать модельную ошибку [ключ, значение]
                ModelState.AddModelError("", "Вы не добавили товары!");
            }

            //IsValid вернёт true, если все поля введены верно
            if (ModelState.IsValid)
            {
                //оформляем заказ
                allOrders.createOrder(order);
                return RedirectToAction("Complete");
            }

            //неверная форма. Все данные вставятся в фрму и сохранятся даже
            //после перезагрузки
            return View(order);
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ принят";

            return View();
        }
    }
}
