using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestRestaurants.Models;
using RestaurantApp;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Tests
{

  [TestClass]

  public class RestaurantTests : IDisposable
  {
    public RestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurants_test;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyFirst_0()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
  }
}
