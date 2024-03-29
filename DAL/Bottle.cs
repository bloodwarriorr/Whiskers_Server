﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Bottle
    {
        public Brand Brand { get; set; }
        public int Barcode { get; set; }
        public string BottleName { get; set; }
        public string Age { get; set; }
        public double Price { get; set; }
        public Type Type { get; set; }
        public Taste Taste { get; set; }
        public string Image { get; set; }
        public double ABV { get; set; }
        public string Description { get; set; }
        public Bottle()
        {

        }

        public Bottle(Brand brand, int barcode, string name, string age, double price, Type type, Taste taste, string image, double aBV, string description)
        {
            Brand = brand;
            Barcode = barcode;
            BottleName = name;
            Age = age;
            Price = price;
            Type = type;
            Taste = taste;
            Image = image;
            ABV = aBV;
            Description = description;
        }
    }
}
