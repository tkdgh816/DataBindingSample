using System.IO;

using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

using Windows.ApplicationModel;

namespace DataBindingSample
{
  public sealed partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      var presenter = (OverlappedPresenter)AppWindow.Presenter;
      presenter.PreferredMinimumWidth = 800;
      presenter.PreferredMinimumHeight = 1000;
      AppWindow.Resize(new(800, 1000));
      AppWindow.SetIcon(Path.Combine(Package.Current.InstalledLocation.Path, "Assets/Icon/AppIcon_128.ico"));
    }
  }
}
