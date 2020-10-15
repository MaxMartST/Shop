using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class ShopCartItem
    {
        //данная модель отвечает за конкретный товар в карзине
        public int id { get; set; }
        public Car car { get; set; }
        public int price { get; set; }
        //id товара внутри карзины
        public string ShopCartId { get; set; }
    }
}
