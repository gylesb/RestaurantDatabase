using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BestRestaurants.Models
{
  public class Cuisine
  {
    private string _name;
    private int _id;

    public Cuisine(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        return this.GetId().Equals(newCuisine.GetId());
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public List<Restaurant> GetRestaurants()
    {
      List<Restaurant> allCuisineRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = @cuisine_id;";

      MySqlParameter cuisineId = new MySqlParameter();
      cuisineId.ParameterName = "@cuisine_id";
      cuisineId.Value = this._id;
      cmd.Parameters.Add(cuisineId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantType = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(3);
        int restaurantPrice = rdr.GetInt32(4);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantType, restaurantCuisineId, restaurantPrice, restaurantId);
        allCuisineRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisineRestaurants;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisine (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int CuisineId = rdr.GetInt32(0);
        string CuisineName = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisines;
    }

    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int CuisineId = 0;
      string CuisineName = "";

      while(rdr.Read())
      {
        CuisineId = rdr.GetInt32(0);
        CuisineName = rdr.GetString(1);
      }

      Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return newCuisine;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisine;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void DeleteCuisine()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisine WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
  }

}
