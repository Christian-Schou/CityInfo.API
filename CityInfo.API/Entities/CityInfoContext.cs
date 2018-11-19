﻿// Christian Schou
// CityInfoContext.cs
//
using System;
using Microsoft.EntityFrameworkCore;
namespace CityInfo.API.Entities
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionsstring");

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
