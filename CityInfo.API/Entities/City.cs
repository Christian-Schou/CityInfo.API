// Christian Schou
// City.cs
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class City
    {
        // Primary key in database
        // Identity creation for "on add" - a new key will be generated, 
        // when a city is added
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; }

        // Inistalize this to an empty list to avoid null reference exceptions
        // when trying to manipulate that list, when the points of interest
        // haven't been loaded yet.

               = new List<PointOfInterest>();
    }
}
