using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Repository
{
    public class OrdersRepository : IAllOrders
    {
        //для внесения изменения данных в БД
        private readonly AppDBContent _appDBContent;
        //все товары пользователя из казины
        private readonly ShopCart _shopCart;
        //конструктор
        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCart)
        {
            _appDBContent = appDBContent;
            _shopCart = shopCart;
        }
        public void createOrder(Order order)
        {
            order.orderTime = DateTime.Now;
            //добавляем заказ в таблицу БД

            var detailList = new List<OrderDetail>();
            detailList.Add(new OrderDetail()
            {
                orderID = 0
   
            });
            order.orderDetail = detailList;

            //переменная которая хранитте товары, которые
            //приобретает пользователь. Взять из карзины
            var items = _shopCart.listShopItems;
            foreach (var elem in items)
            {
                var orderDatail = new OrderDetail()
                {
                    CarID = elem.car.id,
                    price = elem.car.price
                };

                detailList.Add(orderDatail);
            }

            //сохранить изменения в БД
            _appDBContent.SaveChanges();
        }
    }
}
