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
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Cuisine> cuisineList = Cuisine.GetAll();
      List<Restaurant> cuisineRestaurants = Restaurant.GetAll();
      model.Add("cuisine", cuisineList);
      model.Add("restaurant", cuisineRestaurants);

      return View("Index", model);
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
      Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-type"], id, int.Parse(Request.Form["restaurant-price"]));
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

    [HttpPost("/restaurant/{name}/{id}/review")]
    public ActionResult AddReviewViewRestaurantReviews(int id)
    {
      Review newReview = new Review(Request.Form["review-description"], id ,Request.Form["reviewer"]);
      newReview.Save();
      Dictionary<string, object> model = new Dictionary<string, object>();
      Restaurant selectedRestaurant = Restaurant.Find(id);
      List<Review> restaurantReviews = selectedRestaurant.GetReview();

      model.Add("restaurant", selectedRestaurant);
      model.Add("review", restaurantReviews);

      return View("RestaurantReview", model);
    }

    [HttpGet("/restaurant/{name}/{id}/review")]
    public ActionResult ViewRestaurantReviews(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Restaurant selectedRestaurant = Restaurant.Find(id); //Restaurant is selected as an object
      List<Review> restaurantReviews = selectedRestaurant.GetReview(); //Reviews of the selected restaurant are displayed in a list

      model.Add("restaurant", selectedRestaurant);
      model.Add("review", restaurantReviews);

      //return the restaurant list for selected cuisine
      return View("RestaurantReview", model);
    }

    [HttpGet("/restaurants/{id}/review/add")]
    public ActionResult AddReview(int id)
    {
      Restaurant selectedRestaurant = Restaurant.Find(id);
      return View(selectedRestaurant);
    }

    [HttpGet("{restaurant_id}/review/{id}/edit")]
    public ActionResult ReviewEdit(int restaurant_id, int id)
    {
      Review thisReview = Review.Find(id);

      Dictionary<string, object> model = new Dictionary<string, object>();
      Restaurant selectedRestaurant = Restaurant.Find(restaurant_id); //Restaurant is selected as an object

      model.Add("restaurant", selectedRestaurant);
      model.Add("review", thisReview);

      return View("ReviewEdit", model);
    }

    [HttpPost("{restaurant_id}/review/{id}/edit")]
    public ActionResult PostReview(int restaurant_id, int id)
    {

      Review thisReview = Review.Find(id);
      thisReview.UpdateReview(Request.Form["new-review"]);

      Dictionary<string, object> model = new Dictionary<string, object>();
      Restaurant selectedRestaurant = Restaurant.Find(restaurant_id); //Restaurant is selected as an object
      List<Review> restaurantReviews = selectedRestaurant.GetReview(); //Reviews of the selected restaurant are displayed in a list

      model.Add("restaurant", selectedRestaurant);
      model.Add("review", restaurantReviews);

      return View("RestaurantReview", model);
    }

    [HttpGet("/{restaurant_id}/review/{id}/delete")]
    public ActionResult ReviewDeleteConfirm(int restaurant_id, int id)
    {
      Review thisReview = Review.Find(id);
      thisReview.DeleteReview();

      Dictionary<string, object> model = new Dictionary<string, object>();
      Restaurant selectedRestaurant = Restaurant.Find(restaurant_id); //Restaurant is selected as an object
      List<Review> restaurantReviews = selectedRestaurant.GetReview(); //Reviews of the selected restaurant are displayed in a list

      model.Add("restaurant", selectedRestaurant);
      model.Add("review", restaurantReviews);

      return View("RestaurantReview", model);
    }

  }
}
