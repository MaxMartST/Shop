﻿using Shop.Data.Interfaces;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private IAllCars _carRep;

        public HomeController(IAllCars carRep)
        {
            _carRep = carRep;
        }

        //возвращаем шаблон
        public ViewResult Index()
        {
            var honeCars = new HomeViewModel
            {
                favCars = _carRep.getFavCars
            };
            return View(honeCars);
        }
    }
}
