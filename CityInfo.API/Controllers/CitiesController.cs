using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            // find city
            var cityToReturn = 
                CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            // If city is not found
            // we will return a 404 Not found HTTP status code
            if(cityToReturn == null)
            {
                return NotFound();
            }

            // Else we will return a 
            // HTTP status code 200 with the specified city
            return Ok(cityToReturn);
        }
    }
}
