using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Interfaces
{
    //в интерфнйсах нет реализации функций
    public interface ICarsCategory
    {
        //функция возвращает список category
        IEnumerable<Category> AllCategories { get; }
    }
}
