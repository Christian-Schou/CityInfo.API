// Christian Schou
// CityDto.cs
//
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
        = new List<PointOfInterestDto>();
    }
}