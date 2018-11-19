using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    // Repository implementation which is registered in the configureServices

    public class CityInfoRepository : ICityInfoRepository
    {

        // Through constructor injection, we are sure, that
        // we have an instance of the CityInfoContext
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            // remember .ToList to make sure the query is executed at that specific moment
            // Calling tolist means iteration and for that to happen we have to make a query
            return _context.Cities.OrderBy(x => x.Name).ToList();
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            // If the poi should be included, we will return POIs for the specified cityId
            if (includePointsOfInterest)
            {
                return _context.Cities
                    .Include(x => x.PointsOfInterest).FirstOrDefault(x => x.Id == cityId);
            }

            // If the points of interest should be excluded, then we will
            // return only the specified city.
            return _context.Cities.FirstOrDefault(x => x.Id == cityId);
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            // Returning all the points of interest, where the CityId = cityId
            return _context.PointsOfInterest.Where(p => p.CityId == cityId).ToList();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            // Returning specific POI for specific city
            return _context.PointsOfInterest
                .FirstOrDefault(p => p.CityId == cityId && p.Id == pointOfInterestId);
        }
    }
}
