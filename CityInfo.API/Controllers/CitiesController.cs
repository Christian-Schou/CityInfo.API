﻿using System;
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

        /// <summary>
        /// This will let you see all cities in the database.
        /// </summary>
        /// <returns>Returns a result with cities in the database.</returns>
        [HttpGet()]
        public IActionResult GetCities()
        {
            // return Ok(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            // Use the automapper, so we don't have to write all the mappings manually
            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            return Ok(results);
        }

        /// <summary>
        /// This allows you to get a specific city and choose to get points of interest for that city
        /// </summary>
        /// <param name="id">City Id</param>
        /// <param name="includePointsOfInterest">Include point of interest</param>
        /// <returns>Returns a specific city with or without points of interest</returns>
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
