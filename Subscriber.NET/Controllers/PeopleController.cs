using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Subscriber.NET.Models;

namespace Subscriber.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ILogger<PeopleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Person
            {
                Age = 50,
                Name = "Femi Joseph",
                Address = "Kano, Yo Mama"
            })
            .ToArray();
        }
    }
}
