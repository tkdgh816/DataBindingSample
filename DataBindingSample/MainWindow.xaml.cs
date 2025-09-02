using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

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
    }
  }
}
