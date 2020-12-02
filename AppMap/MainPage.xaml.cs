using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AppMap.MapViewModel;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using AppMap.MapModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using AppMap.MapView;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppMap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MapVM MapViewModel { get; set; }

        public MainPage()
        {
            this.MapViewModel = new MapVM();
            MapViewModel.SelectedLocationChangedEvent += MapViewModel_SelectedLocationChangedEvent;
            this.InitializeComponent();
        }

        private async void MapViewModel_SelectedLocationChangedEvent(MapIcon element)
        {
            await myMap.TrySetViewAsync(element.Location);
        }

        private void MyMap_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements[0] as MapIcon;
            MapViewModel.EditDialog(mapIcon);
        }

    }
}
