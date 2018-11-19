using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CityInfo.API.Models;
using CityInfo.API.Services;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            // return Ok(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            var results = new List<CityWithoutPointsOfInterestDto>();

            foreach (var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                });
            }

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            // First thing to check - did we actually get something back?
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            // if we didn't return a 404 NotFound HTTP status code
            if (city == null)
            {
                return NotFound();
            }

            // We have to map this result. Either to a CityDto or a CityWithoutPointsOfInterestDto

            // First we check the included value for the points of interest value
            // if it had to be included, then
            if (includePointsOfInterest)
            {
                // We will map to a CityDto, then
                var cityResult = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                // Run through the points of interest and map each point of interest to
                // a PointOfInterestDto, then
                foreach (var poi in city.PointsOfInterest)
                {
                    cityResult.PointsOfInterest.Add(
                        new PointOfInterestDto()
                        {
                            Id = poi.Id,
                            Name = poi.Name,
                            Description = poi.Description
                        });
                }

                // We return the results
                return Ok(cityResult);
            }

            // If they don't have to be included
            // We map to a CityWithoutPointsOfInterestDto

            var cityWithoutPointsOfInterestResult =
                new CityWithoutPointsOfInterestDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

            // After that, we return that dto
            return Ok(cityWithoutPointsOfInterestResult);

            //// find city
            //var cityToReturn = 
            //    CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            //// If city is not found
            //// we will return a 404 Not found HTTP status code
            //if(cityToReturn == null)
            //{
            //    return NotFound();
            //}

            //// Else we will return a 
            //// HTTP status code 200 with the specified city
            //return Ok(cityToReturn);
        }
    }
}
