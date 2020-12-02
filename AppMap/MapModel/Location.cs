using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMap.MapModel {
  public class Location {
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Altitude { get; set; }

    public string Name { get; set; } = "Marker";

    public Location(double latitude, double longitude, double altitude) {
      Latitude = latitude;
      Longitude = longitude;
      Altitude = altitude;
    }
  }
}
