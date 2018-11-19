// Christian Schou
// PointOfInterest.cs
//
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        // Primary key in database
        // Identity creation for "on add" - a new key will be generated, 
        // when a point of interest is added

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        // Navigation property - the forerign key
        // its a depended class. Also called a convetion based approach

        [ForeignKey("CityId")]
        public City city { get; set; }
        public int CityId { get; set; }
    }
}
