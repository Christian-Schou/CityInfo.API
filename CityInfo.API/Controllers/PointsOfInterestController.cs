// Christian Schou
// PointsOfInterestController.cs
//
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using AutoMapper;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;
namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;

        // Contructor injection with logging
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, 
            IMailService mailService,
            ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                // If city is not found return a not found status code
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

                var pointsOfInterestForCityResults =
                    Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);

                // Then we return this list of DTOs
                return Ok(pointsOfInterestForCityResults);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city ith id {cityId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [Route("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            // First we will check if the city exists
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            // Lets try get the point of interest for the city
            // if we get null back , we return 404 not found HTTP status
            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            // Otherwise we map the POI to a DTO
            var pointOfInterestResult = Mapper.Map<PointOfInterestDto>(pointOfInterest);

            // Then we return that DTO
            return Ok(pointOfInterestResult);

        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            // Lets us return a response with the location header
            // since the route template needs city id and the id of the
            // point of interest, we will pass in an anonamus type, with those values
            return CreatedAtRoute("GetPointOfInterest", new
            {cityId, id = finalPointOfInterest.Id}, finalPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, 
                                                   [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            // Return a 204 No content HTTP status code
            // means that the request completed succesfully, but we got nothing to return
            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, 
                                                            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestToPatch.Description = pointOfInterestToPatch.Description;

            // Return a 204 No content HTTP status code
            // means that the request completed succesfully, but we got nothing to return
            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            // Remove the point of interest
            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            // Send a notification mail
            _mailService.Send("Point of interest deleted.", 
                              $"Point of interest{pointOfInterestFromStore.Name}with id {pointOfInterestFromStore.Id} was deleted.");

            return NoContent();
        }
    }
}
