using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AppMap.MapView {
  class AltitudeConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, string language) {
      if (value.GetType() != typeof(double) || value == null)
        return null;

      var altitude = (double)value;
      const double toMeters = 0.3048;

      altitude = altitude * toMeters;
      var formatAltitude = altitude.ToString().PadLeft(4, '0');

      return $"{formatAltitude}m";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) {
      throw new NotImplementedException();
    }
  }
}
