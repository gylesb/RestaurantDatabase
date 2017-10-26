using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;

namespace RestaurantApp.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Restaurant> cuisineRestaurants = Restaurant.GetAll();

      return View(cuisineRestaurants);
    }

    [HttpGet("/cuisine/add")]
    public ActionResult AddCuisine()
    {
      return View();
    }

    [HttpPost("/cuisine/list")]
    public ActionResult WriteCuisines()
    {
      Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
      newCuisine.Save();
      List<Cuisine> allCuisines = Cuisine.GetAll();

      return View("ViewCuisine", allCuisines);
    }

    [HttpGet("/cuisine/list")]
    public ActionResult ReadCuisines()
    {
      List<Cuisine> allCuisines = Cuisine.GetAll();

      return View("ViewCuisine", allCuisines);
    }

    [HttpGet("/{name}/{id}/restaurantlist")]
    public ActionResult ViewRestaurantList(int id)
    {
      // Console.WriteLine("Hewwo");

      Dictionary<string, object> model = new Dictionary<string, object>();
      // Cuisine is selected as an object
      Cuisine selectedCuisine = Cuisine.Find(id);
      // Restaurants listed
      List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();

      model.Add("cuisine", selectedCuisine);
      model.Add("restaurants", cuisineRestaurants);

      // Return the restaurant list
      return View("CuisineDetail", model);
    }

    [HttpGet("/{name}/{id}/restaurant/add")]
    public ActionResult AddRestaurant(int id)
    {
      // Cuisine is selected as an object
      Cuisine selectedCuisine = Cuisine.Find(id);

      return View(selectedCuisine);
    }

    [HttpPost("/{name}/{id}/restaurantlist")]
    public ActionResult AddRestaurantViewRestaurantList(int id)
    {
      Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-type"], id, Request.Form["restaurant-price"]);
      newRestaurant.Save();
      Dictionary<string, object> model = new Dictionary<string, object>();
      // Cuisine is selected as an object
      Cuisine selectedCuisine = Cuisine.Find(id);
      // Restaurants are displayed in a list
      List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
      //Console.WriteLine(id);

      model.Add("cuisine", selectedCuisine);
      model.Add("restaurants", cuisineRestaurants);
      // Console.WriteLine(cuisineRestaurants[0].GetDescription());

      // Return restaurant list for selected cuisine
      return View("CuisineDetail", model);
    }

    [HttpGet("/restaurants/{id}/edit")]
    public ActionResult RestaurantEdit(int id)
    {
      Restaurant thisRestaurant = Restaurant.Find(id);

      return View(thisRestaurant);
    }

    [HttpPost("/restaurants/{id}/edit")]
    public ActionResult RestaurantEditConfirm(int id)
    {
      Restaurant thisRestaurant = Restaurant.Find(id);
      thisRestaurant.UpdateName(Request.Form["new-name"]);

      return RedirectToAction("Index");
    }

    [HttpGet("/{name}/{id}/restaurant/delete")]
    public ActionResult RestaurantDelete(int id)
    {
      // Cuisine is selected as an object
      Restaurant thisRestaurant = Restaurant.Find(id);
      thisRestaurant.DeleteRestaurant();

      return RedirectToAction("Index");
    }

    [HttpGet("/{name}/{id}/cuisine/delete")]
    public ActionResult CuisineDelete(int id)
    {
      // Cuisine is selected as an object
      Cuisine thisCuisine = Cuisine.Find(id);
      thisCuisine.DeleteCuisine();

      return RedirectToAction("Index");
    }
  }
}
