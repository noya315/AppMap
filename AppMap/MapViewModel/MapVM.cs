using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using AppMap.MapModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using System.Collections.ObjectModel;
using AppMap.MapView;
using Windows.UI.Xaml.Controls;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Popups;

namespace AppMap.MapViewModel {
  public delegate void OnSelectedLocationChange(MapIcon element);

  class MapVM {
    private MapIcon selectedLocation = null;

    public List<MapElement> Elements { get; } = new List<MapElement>();

    public ObservableCollection<MapLayer> Layers { get; } =
        new ObservableCollection<MapLayer>();

    public event OnSelectedLocationChange SelectedLocationChangedEvent;


    public MapIcon SelectedLocation
    {
      get { return selectedLocation; }
      set
      {
        selectedLocation = value;
        SelectedLocationChangedEvent?.Invoke(SelectedLocation);
      }
    }

    public MapVM() { }

    public async Task<Location> GetLocation() {
      const string serviceUrl = "http://geoposition.000webhostapp.com/";
      var client = new HttpClient();
      var response = await client.GetStringAsync(serviceUrl);

      DefaultContractResolver contractResolver = new DefaultContractResolver
      {
        NamingStrategy = new CamelCaseNamingStrategy()
      };

      return JsonConvert.DeserializeObject<Location>(response, new JsonSerializerSettings
      {
        ContractResolver = contractResolver
      });
    }


    public MapIcon AddRandomLocation() {
      var getLocation = GetLocation();
      getLocation.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
      {
        if (getLocation.IsFaulted)
          ShowErrorMessage();
        else {
          var location = getLocation.Result;
          AddLocation(location);
        }
      });
      return SelectedLocation;
    }

    public void EditDialog(MapIcon position) {
      SelectLocation(position);
      OpenEditDialog();
    }

    private void SelectLocation(MapIcon element) {
      SelectedLocation = element;
    }

    private void AddLocation(Location location) {

      BasicGeoposition position = new BasicGeoposition
      {
        Latitude = location.Latitude,
        Longitude = location.Longitude,
        Altitude = location.Altitude
      };

      Geopoint point = new Geopoint(position);
      var marker = new MapIcon
      {
        Location = point,
        NormalizedAnchorPoint = new Point(0.5, 1.0),
        Title = location.Name,
        ZIndex = 0,
      };


      Elements.Add(marker);
      var LandmarksLayer = new MapElementsLayer
      {
        ZIndex = 1,
        MapElements = Elements
      };
      Layers.Add(LandmarksLayer);
      SelectLocation(marker);
    }

    private async void OpenEditDialog() {
      AppWindow appWindow = await AppWindow.TryCreateAsync();
      Frame appWindowContentFrame = new Frame();
      appWindow.Title = "Marker Infocard Window";
      appWindow.RequestSize(new Size(200, 250));
      appWindowContentFrame.Navigate(typeof(EditPage), SelectedLocation);
      ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);
      await appWindow.TryShowAsync();
    }

    private async void ShowErrorMessage() {
      MessageDialog showDialog = new MessageDialog("Oops! Something went wrong...", "Error");
      showDialog.Commands.Add(new UICommand("Close"));
      await showDialog.ShowAsync();
    }
  }

}
