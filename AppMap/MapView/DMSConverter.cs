using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AppMap.MapView {
  class DMSConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, string language) {
      if (value.GetType() != typeof(double) || value == null)
        return null;

      // algorithem for convert lat / lng to DMS format
      var latitude = (double)value;
      latitude = Math.Abs(latitude);
      var degrees = Math.Truncate(latitude);
      latitude = (latitude - degrees) * 60;
      var minutes = Math.Truncate(latitude);
      var seconds = Math.Truncate((latitude - minutes) * 60);

      //pad results with leading zeroes
      var padDegress = PadNumberWithLeadingZeroes(degrees.ToString(), 3);
      var padMinutes = PadNumberWithLeadingZeroes(minutes.ToString(), 2);
      var padSeconds = PadNumberWithLeadingZeroes(seconds.ToString(), 2);

      return $"{padDegress}°{padMinutes}'{padSeconds}''";

    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) {
      throw new NotImplementedException();
    }

    private string PadNumberWithLeadingZeroes(string number, int totalWidth) {
      const char pad = '0';
      return number.PadLeft(totalWidth, pad);
    }
  }
}
