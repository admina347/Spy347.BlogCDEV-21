using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Spy347.BlogCDEV_21.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Метод для получения информации о доме
        /// </summary>
        [HttpGet]
        [Route("info")] 
        public IActionResult Info()
        {
            // Получим запрос, смапив конфигурацию на модель запроса
            //var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);
            // Вернём ответ
            return StatusCode(200);
        }

        /* [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        } */
    }
}