using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;

namespace CityInfo.API
{
    public static class CityInfoContextExtensions
    {
        // This seeding will be executing from startup.cs

        // "this" tells the compiler it extends CityInfoContext 
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            // if database contain data, we will not insert new data
            if (context.Cities.Any())
            {
                return;
            }

            // init seed data

            var cities = new List<City>()
            {

                // Odense with two POIs
                new City()
                {
                    Name = "Odense",
                    Description = "Odense er Danmarks tredjestørste og Fyns største by med 178.210 indbyggere. Byen ligger ved Odense Å, cirka 3 kilometer syd for Odense Fjord.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "H.C. Andersens Hus",
                            Description = "H.C. Andersens Hus i Odense er hovedmuseet for eventyrdigteren og forfatteren H.C. Andersen. Huset blev købt af Odense Kommune i 1905 på 100-året for H.C. Andersens fødsel."
                        },

                        new PointOfInterest()
                        {
                            Name = "Odeon",
                            Description = "ODEON er et musik-, teater- og konferencehus i Odense, med central beliggenhed i det store byomdannelsesprojekt i midtbyen, tæt på det gamle H.C. Andersen-kvarter og Odense Koncerthus."
                        },
                    }
                },

                // Copenhagen with two POIs
                new City()
                {
                    Name = "København",
                    Description = "København er Danmarks hovedstad og er med 1.308.893 indbyggere landets største byområde omfattende 18 kommuner eller dele heraf.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Tivoli",
                            Description =  "Den fortryllende have som byder på alt fra forlystelser, restauranter, teaterforestillinger og koncerter."
                        },

                        new PointOfInterest()
                        {
                            Name = "Rundetårn",
                            Description = "Rundetårn er et 41,55 meter højt observationstårn, der ligger i Købmagergade i Indre By, København. Højden på 34,8 meter refererer kun til udsigtsplatformens højde over gadeniveau."
                        },
                    }
                },

                // Aalborg with two POIs
                new City()
                {
                    Name = "Aalborg",
                    Description = "Aalborg eller Ålborg er en by i Region Nordjylland med 114.194 indbyggere, som derfor er Danmarks fjerdestørste by. ",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "AaB Fodbold",
                            Description = "AaB er en dansk fodboldklub hjemmehørende i Aalborg og den eneste professionelle del af Aalborg Boldspilklub, der rummer flere forskellige sportsafdelinger."
                        },

                        new PointOfInterest()
                        {
                            Name = "Aalborg Zoo",
                            Description = "Aalborg Zoo er en bynær dyrepark, der ligger nær Aalborgs centrum. Aalborg Zoo besøges på årsbasis af omkring 400.000 gæster, og parken rummer mere end 1.500 dyr."
                        },
                    }
                }
            };

            // Lets add the cities to the context
            context.Cities.AddRange(cities);

            // Then save them to database
            context.SaveChanges();

        }
    }
}
