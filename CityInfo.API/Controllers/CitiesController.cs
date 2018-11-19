using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
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

            // Use the automapper, so we don't have to write all the mappings manually
            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

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
            
            // First we check the included value for the points of interest value
            // if it had to be included, then
            if (includePointsOfInterest)
            {
                var cityResult = Mapper.Map<CityDto>(city);

                // We return the results
                return Ok(cityResult);
            }

            // If they don't have to be included
            // We map to a CityWithoutPointsOfInterestDto with Automapper

            var cityWithoutPointsOfInterestResult = Mapper.Map<CityWithoutPointsOfInterestDto>(city);

            // After that, we return that dto
            return Ok(cityWithoutPointsOfInterestResult);
        }
    }
}
