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

        return (nameEquality && typeEquality && cuisineEquality && priceEquality && idEquality);
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurants (name, type, cuisine_id, price) VALUES (@name, @type, @cuisine_id, @price);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@type";
      type.Value = this._type;
      cmd.Parameters.Add(type);

      MySqlParameter cuisine_id = new MySqlParameter();
      cuisine_id.ParameterName = "@cuisine_id";
      cuisine_id.Value = this._cuisineId;
      cmd.Parameters.Add(cuisine_id);

      MySqlParameter price = new MySqlParameter();
      price.ParameterName = "@price";
      price.Value = this._price;
      cmd.Parameters.Add(price);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
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
        string restaurantType = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(3);
        int restaurantPrice = rdr.GetInt32(4);

        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantType, restaurantCuisineId, restaurantPrice, restaurantId);
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

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `restaurants` WHERE id = @thisId ORDER BY id DESC;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int restaurantId = 0;
      string restaurantName = "";
      int restaurantCuisineId = 0;
      string restaurantType = "";
      int restaurantPrice = 0;

      while (rdr.Read())
      {
        restaurantId = rdr.GetInt32(0);
        restaurantName = rdr.GetString(1);
        restaurantType = rdr.GetString(2);
        restaurantCuisineId = rdr.GetInt32(3);
        restaurantPrice = rdr.GetInt32(4);
      }

      Restaurant newRestaurant = new Restaurant(restaurantName, restaurantType, restaurantCuisineId, restaurantPrice, restaurantId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }

    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE restaurants SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
  }
}
