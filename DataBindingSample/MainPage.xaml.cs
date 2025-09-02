using Microsoft.UI.Xaml.Controls;

namespace DataBindingSample;

public sealed partial class MainPage : Page
{
  public MainPage()
  {
    InitializeComponent();
  }

  private void View_NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
  {
    var selectedItem = (NavigationViewItem)args.SelectedItem;
    if (selectedItem == View_Binding_NavigationViewItem)
      View_Frame.Navigate(typeof(BindingPage));
    else if (selectedItem == View_xBind_NavigationViewItem)
      View_Frame.Navigate(typeof(xBindPage));
  }

  private void View_NavigationView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    View_NavigationView.SelectedItem = View_Binding_NavigationViewItem;
  }
}
