﻿using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("[controller]")]
    public class LogsController : Controller
    {
        public IActionResult Index() =>
            View();
    }
}
