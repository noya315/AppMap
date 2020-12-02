using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppMap.MapView
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class EditPage : Page
  {
    public MapIcon SelectedLocation
    {
      get { return (MapIcon)GetValue(SelectedLocationProperty); }
      set { SetValue(SelectedLocationProperty, value); }
    }

    public static readonly DependencyProperty SelectedLocationProperty =
        DependencyProperty.Register("SelectedLocation", typeof(MapIcon), typeof(EditPage), new PropertyMetadata(null));

    public EditPage() {
      this.InitializeComponent();
    }

    private void LocationName_TextChanged(object sender, TextChangedEventArgs e) {
      SelectedLocation.Title = locationName.Text;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      SelectedLocation = (MapIcon)e.Parameter;
    }
  }
}
