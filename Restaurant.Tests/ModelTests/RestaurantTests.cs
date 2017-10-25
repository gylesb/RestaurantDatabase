using Microsoft.VisualStudio.TestTools.UnitTesting;
using BestRestaurants.Models;
using RestaurantApp;
using System.Collections.Generic;
using System;

namespace BestRestaurants.Tests
{

  [TestClass]

  public class RestaurantsTest : IDisposable
  {
    public RestaurantsTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_test;";
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

    [TestMethod]
    public void Equals_OverrideTrueIfNamesAreSame_Restaurant()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Pizza Hut", "Italian", 1, 20);
      Restaurant secondRestaurant = new Restaurant("Pizza Hut", "Italian", 1, 20);

      //Assert
      Assert.AreEqual(firstRestaurant, secondRestaurant);
    }

    [TestMethod]
    public void Save_SavesToDatabase_RestaurantList()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Pizza Hut", "Italian", 1, 20, 1);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Assert
      Restaurant testRestaurant = new Restaurant("Pizza Hut", "Italian", 1, 20);

      //Act
      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindRestaurantInDatabase_Restaurant()
    {
      // Arrange
      Restaurant testRestaurant = new Restaurant("Diner Yukihira", "Japanese", 1, 30);
      testRestaurant.Save();

      // Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      // Assert
      Assert.AreEqual(testRestaurant, foundRestaurant);
    }

    [TestMethod]
    public void Update_UpdatesRestaurantInDatabase_String()
    {
      // Arrange
      string name = "Trattoria Aldini";
      Restaurant testRestaurant = new Restaurant(name, "Italian", 3, 10);
      testRestaurant.Save();
      string newName = "Osteria La Spriga";

      // Act
      testRestaurant.UpdateName(newName);

      string result = Restaurant.Find(testRestaurant.GetId()).GetName();

      // Assert
      Assert.AreEqual(newName, result);
    }
  }
}
