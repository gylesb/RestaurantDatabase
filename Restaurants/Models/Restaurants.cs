using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisineId;
    private int _price;
    private string _type;

    public Restaurant(string name, string type, int cuisineId, int price, int Id = 0)
    {
      _id = Id;
      _cuisineId = cuisineId;
      _price = price;
      _name = name;
      _type = type;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool typeEquality = (this.GetType() == newRestaurant.GetType());
        bool cuisineEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        bool priceEquality = (this.GetPrice() == newRestaurant.GetPrice());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());

        return (idEquality && typeEquality && cuisineEquality && priceEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetType().GetHashCode();
    }

    public string GetType()
    {
      return _type;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetPrice()
    {
      return _price;
    }

    public int GetCuisineId()
    {
      return _cuisineId;
    }

    public string GetName()
    {
      return _name;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int restaurantCuisineId = rdr.GetInt32(2);
        int restaurantPrice = rdr.GetInt32(3);
        string restaurantType = rdr.GetString(4);

        Restaurant newRestaurant = new Restaurant(restaurantType, restaurantName, restaurantCuisineId, restaurantPrice, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
