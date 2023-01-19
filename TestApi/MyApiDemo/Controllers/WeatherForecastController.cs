using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApiDemo.Model;
using MyApiDemo.Models;

namespace MyApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ProductDbContext _ProductDbContext;
        public WeatherForecastController(ProductDbContext productDbContext)
        {
            _ProductDbContext = productDbContext;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            return await _ProductDbContext.Employees.ToListAsync();
        }
    }
}
