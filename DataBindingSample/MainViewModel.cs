using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace DataBindingSample;

public class MainViewModel : ObservableObject
{
  private string _main_s1_text = "";
  public string Main_S1_Text
  {
    get => _main_s1_text;
    set => SetProperty(ref _main_s1_text, value);
  }

  private double _main_S2_SliderValue = 100.0;
  public double Main_S2_SliderValue
  {
    get => _main_S2_SliderValue;
    set => SetProperty(ref _main_S2_SliderValue, value);
  }

  public string Main_S3_Text1 { get; set; } = "This object is NOT observable.";

  private string _main_S3_Text2 = "This object is observable.";
  public string Main_S3_Text2
  {
    get => _main_S3_Text2;
    set => SetProperty(ref _main_S3_Text2, value);
  }

  public string[] Main_S3_Array { get; } =
  {
    "Array value at index 0.",
    "Array value at index 1.",
    "Array value at index 2."
  };
  public string[][] Main_S3_JaggedArray { get; } =
  {
    new string[2] { "Array value at [0][0].", "Array value at [0][1]." },
    new string[3] { "Array value at [1][0].", "Array value at [1][1].", "Array value at [1][2]." },
    new string[1] { "Array value at [2][0]." },
  };
  public List<string> Main_S3_List { get; } = new()
  {
    "List value at index 0.",
    "List value at index 1.",
    "List value at index 2.",
    "List value at index 3."
  };
  public Dictionary<string, string> Main_S3_Dictionary { get; } = new()
  {
    { "First", "Dictionary Value for Key 'First'." },
    { "Second", "Dictionary Value for Key 'Second'." },
    { "Third", "Dictionary Value for Key 'Third'." },
    { "Fourth", "Dictionary Value for Key 'Fourth'." },
  };
  public Dog Main_S3_Dog { get; } = new("Dog", true, 300);

  private string _main_S5_Text1 = "";
  public string Main_S5_Text1
  {
    get => _main_S5_Text1;
    set => SetProperty(ref _main_S5_Text1, value);
  }

  private string _main_S5_Text2 = "";
  public string Main_S5_Text2
  {
    get => _main_S5_Text2;
    set => SetProperty(ref _main_S5_Text2, value);
  }

  private string _main_S5_Text3 = "";
  public string Main_S5_Text3
  {
    get => _main_S5_Text3;
    set => SetProperty(ref _main_S5_Text3, value);
  }

  private Dog? _main_S7_Dog;
  public Dog? Main_S7_Dog
  {
    get => _main_S7_Dog;
    set => SetProperty(ref _main_S7_Dog, value);
  }

  public ObservableCollection<Dog> Main_S8_Dogs { get; } = new()
  {
    new Dog("Dog 1", false, 801) { Description = "Dog 1 description" },
    new Dog("Dog 2", true, 802) { Description = "Dog 2 description" },
    new Dog("Dog 3", true, 803)
  };

  public ObservableCollection<Cat> Main_S8_Cats { get; } = new()
  {
    new Cat("Cat 1", true, "A-801") { Description = "Cat 1 description" },
    new Cat("Cat 2", false, "A-802"),
    new Cat("Cat 3", true, "A-803") { Description = "Cat 3 description" },
  };

  public Visibility ToVisibilityInstance(bool boolValue) => boolValue ? Visibility.Visible : Visibility.Collapsed;

  public static Visibility ToVisibilityStatic(bool boolValue) => boolValue ? Visibility.Visible : Visibility.Collapsed;

  private string _main_S10_Text1 = "";
  public string Main_S10_Text1
  {
    get => _main_S10_Text1;
    set => SetProperty(ref _main_S10_Text1, value);
  }

  private string _main_S10_Text2 = "";
  public string Main_S10_Text2
  {
    get => _main_S10_Text2;
    set => SetProperty(ref _main_S10_Text2, value);
  }

  private DateTimeOffset _main_S10_DateTimeOffset = DateTimeOffset.Now;
  public DateTimeOffset Main_S10_DateTimeOffset
  {
    get => _main_S10_DateTimeOffset;
    set => SetProperty(ref _main_S10_DateTimeOffset, value);
  }

  private SolidColorBrush? _main_S10_Brush;
  public SolidColorBrush? Main_S10_Brush
  {
    get => _main_S10_Brush;
    set => SetProperty(ref _main_S10_Brush, value);
  }

  public void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
  {
    Main_S10_Brush = new SolidColorBrush(args.NewColor);
  }

  public Animal Main_S11_Animal { get; } = new Cat("Cat", true, "A-1100");

  public List<Animal> Main_S11_Animals { get; } = new()
  {
    new Dog("Dog 1", false, 1001) { Description = "Dog 1 description" },
    new Dog("Dog 2", true, 1002) { Description = "Dog 2 description" },
    new Dog("Dog 3", true, 1003)
  };
}