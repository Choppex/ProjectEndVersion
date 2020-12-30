using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuthSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // GET: api/<WeatherController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WeatherController>/5
        [HttpGet("{City}")]
        public string Get(string City)
        {
            //Api key
            string appId = "f7cf07c5e77d330f7fd11097457a061f";

            //API path with CITY.  
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}",
                City, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                return json;
            }
        }

        // POST api/<WeatherController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WeatherController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeatherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
