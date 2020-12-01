using Advent2020.DotNetCoreSolution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advent2020.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DotNetDayController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            var runner = new DayRunner();
            var answerString = runner.RunDay(id);
            var returnArray = new List<string>
            {
                answerString
            };
            return returnArray;
        }
    }
}
