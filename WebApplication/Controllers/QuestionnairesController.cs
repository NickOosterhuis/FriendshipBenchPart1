﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class QuestionnairesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}