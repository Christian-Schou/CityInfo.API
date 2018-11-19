// Christian Schou
// CitiesDataStore.cs
//
using System;
using System.Collections.Generic;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {

        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

          public CitiesDataStore()
          {

            // Lets make some dummy data
            Cities = new List<CityDto>()
              {

                new CityDto()
                {
                    Id = 1,
                    Name = "Odense",
                    Description = "En stor by midt i Danmark",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "H.C Andersens hus",
                            Description = "Et hus H.C Andersen boede i da han var barn."
                        },

                        new PointOfInterestDto(){
                            Id = 2,
                            Name = "Gågaden",
                            Description = "Et sted med mange butikker, som prøver at stjæle dine penge."
                        }
                    }
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "København",
                    Description = "Danmarks hovedstad",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "Tivoli",
                            Description = "Et sted for børn og voksne med forlystelser."
                        },

                        new PointOfInterestDto(){
                            Id = 2,
                            Name = "Rundetårn",
                            Description = "Et højt tårn for turister bygget af Christian d. 4 i sin tid."
                        }
                    }
                },

                new CityDto()
                {
                    Id = 3,
                    Name = "Ålborg",
                    Description = "En stor by i det nordlige Jylland",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto(){
                            Id = 1,
                            Name = "Jomfru ane gade",
                            Description = "Stedet hvor man går i byen og drikker sig i hegnet."
                        },

                        new PointOfInterestDto(){
                            Id = 2,
                            Name = "Tivoli",
                            Description = "Nordens Tivoli."
                        }
                    }
                }
            };

        }
    }
}
