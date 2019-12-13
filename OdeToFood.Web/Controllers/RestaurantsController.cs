using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Data.Models;
using OdeToFood.Data.Services;

namespace OdeToFood.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantData _db;

        public RestaurantsController(IRestaurantData db)
        {
            _db = db;
        }
        // GET: Restaurants

        [HttpGet]
        public ActionResult Index()
        {
            var model = _db.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = _db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Edit(restaurant);
                TempData["Message"] = "You have saved the restaurant!";
                return RedirectToAction("Details", new { id = restaurant.Id });
            }

            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken ]
        public ActionResult Create(Restaurant restaurant)
        {
            if (String.IsNullOrEmpty(restaurant.Name))
            {
                ModelState.AddModelError(nameof(restaurant.Name), "The name is required");
            }

            if (ModelState.IsValid)
            {
                _db.Add(restaurant);
                return RedirectToAction("Details", new {id = restaurant.Id});
            }

            return View(); 
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            _db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}