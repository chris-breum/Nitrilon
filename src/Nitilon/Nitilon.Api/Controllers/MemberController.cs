﻿using Microsoft.AspNetCore.Mvc;

namespace Nitilon.Api.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
