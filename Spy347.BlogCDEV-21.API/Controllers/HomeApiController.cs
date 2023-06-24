using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Spy347.BlogCDEV_21.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController : ControllerBase
    {
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
    }
}