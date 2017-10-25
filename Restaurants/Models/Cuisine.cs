using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Restaurant.Models
{
  public class Cuisine
  {
    private string _name;
    private string _id;

    public Cuisine(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

  
  }
}
