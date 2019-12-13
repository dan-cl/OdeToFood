﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdeToFood.Data.Models;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return from x in _db.Restaurants
                orderby x.Name
                select x;
        }

        public Restaurant Get(int id)
        {
           return _db.Restaurants.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Restaurant restaurant)
        { 
            _db.Restaurants.Add(restaurant);
            _db.SaveChanges();
        }

        public void Edit(Restaurant restaurant)
        {
            var entry = _db.Entry(restaurant);
            entry.State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}