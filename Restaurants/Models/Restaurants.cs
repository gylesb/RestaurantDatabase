using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Restaurant.Models
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

    
  }
}
