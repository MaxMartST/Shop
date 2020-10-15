using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Order
    {
        //[атрибуты], позволяющие указать дополнительные свойства поля
        [BindNever]//скрывает поле id
        public int id { get; set; }

        [Display(Name = "Введите имя")]//какое имя должно отображаться
        [StringLength(25)]//проверка: максимальная длина строки 25 символов
        [Required(ErrorMessage = "Длина имени не немние 5 символов")]//выводить сообщение при ошибке
        public string name { get; set; }

        [Display(Name = "Введите фамилию")]
        [StringLength(25)]
        [Required(ErrorMessage = "Длина фамилии не немние 5 символов")]
        public string surName { get; set; }

        [Display(Name = "Введите адрес")]
        [StringLength(45)]
        [Required(ErrorMessage = "Длина адреса не немние 5 символов")]
        public string adress { get; set; }

        [Display(Name = "Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]//проверка на ввод номера телефона
        [StringLength(20)]
        [Required(ErrorMessage = "Длина номера телефона не немние 10 символов")]
        public string phone { get; set; }

        [Display(Name = "Введите свой Email")]
        [DataType(DataType.EmailAddress)]//проевра на ввод email
        [StringLength(25)]
        [Required(ErrorMessage = "Длина Email не немние 15 символов")]//выводить сообщение при ошибке
        public string email { get; set; }

        [BindNever]//скрываем поле
        [ScaffoldColumn(false)]//скрываем поле в исходном коде
        public DateTime orderTime { get; set; }

        public List<OrderDetail> orderDetail { get; set; }
    }
}
