using CommunityToolkit.Mvvm.ComponentModel;

namespace DataBindingSample;

public class SubViewModel : ObservableObject
{
  private string _sub_S1_Text = "";
  public string Sub_S1_Text
  {
    get => _sub_S1_Text;
    set => SetProperty(ref _sub_S1_Text, value);
  }
}
