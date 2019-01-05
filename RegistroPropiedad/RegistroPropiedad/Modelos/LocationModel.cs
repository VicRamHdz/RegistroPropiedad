﻿using System;
namespace RegistroPropiedad.Modelos
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
