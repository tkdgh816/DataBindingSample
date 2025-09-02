using System;

using Microsoft.UI.Xaml.Controls;

namespace DataBindingSample;

public sealed partial class BindingPage : Page
{
  public BindingPage()
  {
    InitializeComponent();
    SetCodeSamples();
  }

  private void View_S3_TextBox_TextChanged(object sender, TextChangedEventArgs e)
  {
    var text = View_S3_TextBox.Text;
    ViewModel.Main_S3_Text1 = text;
    ViewModel.Main_S3_Text2 = text;
  }

  private void View_S5_ExplicitTextBox_KeyUp(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
  {
    if (e.Key == Windows.System.VirtualKey.Enter)
      View_S5_ExplicitTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
  }

  private void View_S7_CreateButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    ViewModel.Main_S7_Dog ??= new("Alex", true, 700);
  }

  private void View_S7_NullButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    ViewModel.Main_S7_Dog = null;
    View_S7_AutoSuggestBox.Text = "";
  }

  private void View_S7_AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
  {
    if (ViewModel.Main_S7_Dog is null)
      return;
    ViewModel.Main_S7_Dog.Description = string.IsNullOrEmpty(args.QueryText) ? null : args.QueryText;
  }

  private void View_S8_DogsRadioButton_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    View_S8_ListView.ItemsSource = ViewModel.Main_S8_Dogs;
  }

  private void View_S8_CatsRadioButton_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    View_S8_ListView.ItemsSource = ViewModel.Main_S8_Cats;
  }

  private void View_S8_AddButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    int index = new Random().Next(100);
    if (View_S8_DogsRadioButton.IsChecked == true)
      ViewModel.Main_S8_Dogs.Add(new Dog($"Dog {index}", index % 2 == 0, 800 + index));
    else if (View_S8_CatsRadioButton.IsChecked == true)
      ViewModel.Main_S8_Cats.Add(new Cat($"Cat {index}", index % 2 == 0, $"A-{800 + index}"));
  }

  private void View_S8_DeleteButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    if (View_S8_ListView.SelectedItem is Animal animal)
    {
      if (View_S8_DogsRadioButton.IsChecked == true)
        ViewModel.Main_S8_Dogs.Remove((Dog)animal);
      else if (View_S8_CatsRadioButton.IsChecked == true)
        ViewModel.Main_S8_Cats.Remove((Cat)animal);
    }
  }
}

public sealed partial class BindingPage : Page
{
  private void SetCodeSamples()
  {
    #region Section 1
    View_S1_1stCodeSample.XamlCode = """
      <TextBox x:Name="View_S1_MainInputTextBox"
               Grid.Column="0"
               Text="{Binding Main_S1_Text, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
               PlaceholderText="Enter main text." />
      <TextBox x:Name="View_S1_SubInputTextBox"
               Grid.Column="1"
               Text="{Binding Source={StaticResource SubViewModel}, Path=Sub_S1_Text, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
               PlaceholderText="Enter sub text." />
      """;
    View_S1_1stCodeSample.XamlHighlights = "Binding";
    View_S1_1stCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        private string _main_s1_text = "";
        public string Main_S1_Text
        {
          get => _main_s1_text;
          set => SetProperty(ref _main_s1_text, value);
        }
      }

      // SubViewModel.cs
      public class SubViewModel : ObservableObject
      {
        private string _sub_S1_Text = "";
        public string Sub_S1_Text
        {
          get => _sub_S1_Text;
          set => SetProperty(ref _sub_S1_Text, value);
        }
      }
      """;

    View_S1_2ndCodeSample.XamlCode = """
      <Page.DataContext>
        <local:MainViewModel x:Name="ViewModel" />
      </Page.DataContext>

      <TextBlock Text="{Binding Text}" />
      """;
    View_S1_2ndCodeSample.XamlHighlights = "Binding";
    View_S1_2ndCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        private string _main_s1_text = "";
        public string Main_S1_Text
        {
          get => _main_s1_text;
          set => SetProperty(ref _main_s1_text, value);
        }
      }
      """;

    View_S1_3rdCodeSample.XamlCode = """
      <Page.Resources>
        <local:SubViewModel x:Key="SubViewModel" />
      </Page.Resources>

      <TextBlock Text="{Binding Source={StaticResource SubViewModel}, Path=Sub_S1_Text}" />
      """;
    View_S1_3rdCodeSample.XamlHighlights = "Binding";
    View_S1_3rdCodeSample.CSharpCode = """
      // SubViewModel.cs
      public class SubViewModel : ObservableObject
      {
        private string _sub_S1_Text = "";
        public string Sub_S1_Text
        {
          get => _sub_S1_Text;
          set => SetProperty(ref _sub_S1_Text, value);
        }
      }
      """;

    View_S1_4thCodeSample.XamlCode = """<TextBlock Text="{Binding ElementName=View_S1_MainInputTextBox, Path=Text}" />""";
    View_S1_4thCodeSample.XamlHighlights = "Binding";
    #endregion

    #region Section 2
    View_S2_1stCodeSample.XamlCode = """
      <Slider x:Name="View_S2_SizeSlider"
              Minimum="50"
              Maximum="150"
              Width="400"
              Value="{Binding Main_S2_SliderValue, Mode=TwoWay}" />
      """;
    View_S2_1stCodeSample.XamlHighlights = "Binding";
    View_S2_1stCodeSample.CSharpCode = """
    // MainViewModel.cs
    public class MainViewModel : ObservableObject
    {
      private double _main_S2_SliderValue = 100.0;
      public double Main_S2_SliderValue
      {
        get => _main_S2_SliderValue;
        set => SetProperty(ref _main_S2_SliderValue, value);
      }
    }
    """;

    View_S2_2ndCodeSample.XamlCode = """
      <Border Background="LightPink"
              Width="{Binding Main_S2_SliderValue}"
              Height="{Binding Main_S2_SliderValue}">
        <TextBlock Text="{Binding Main_S2_SliderValue}" 
                   HorizontalAlignment="Center"
                   VerticalAlignment="Center"/>
      </Border>
      """;
    View_S2_2ndCodeSample.XamlHighlights = "Binding";

    View_S2_3rdCodeSample.XamlCode = """
      <Border x:Name="View_S2_Border"
              Background="LightBlue"
              Width="{Binding ElementName=View_S2_SizeSlider, Path=Value}"
              Height="{Binding ElementName=View_S2_SizeSlider, Path=Value}">
        <TextBlock Text="{Binding ElementName=View_S2_Border, Path=Height}"
                   HorizontalAlignment="Center"
                   VerticalAlignment="Center" />
      </Border>
      """;
    View_S2_3rdCodeSample.XamlHighlights = "ElementName";

    View_S2_4thCodeSample.XamlCode = """
      <Border Background="LightGreen"
              Width="{Binding ElementName=View_S2_SizeSlider, Path=Value}"
              Height="{Binding RelativeSource={RelativeSource Self}, Path=Width}">
        <TextBlock Text="{Binding Main_S2_SliderValue}"
                   HorizontalAlignment="Center"
                   VerticalAlignment="Center" />
      </Border>
      """;
    View_S2_4thCodeSample.XamlHighlights = "RelativeSource";
    #endregion

    #region Section 3
    View_S3_1stCodeSample.XamlCode = """
      <TextBox x:Name="View_S3_TextBox"
               PlaceholderText="Enter text."
               TextChanged="View_S3_TextBox_TextChanged" />
      """;
    View_S3_1stCodeSample.CSharpCode = """
      // BindingPage.xaml.cs
      private void View_S3_TextBox_TextChanged(object sender, TextChangedEventArgs e)
      {
        var text = View_S3_TextBox.Text;
        ViewModel.Main_S3_Text1 = text;
        ViewModel.Main_S3_Text2 = text;
      }
      """;
    View_S3_2ndCodeSample.XamlCode = """
      <TextBlock Text="{Binding Main_S3_Text1}" />
      """;
    View_S3_2ndCodeSample.XamlHighlights = "Binding";
    View_S3_2ndCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        public string Main_S3_Text1 { get; set; } = "This object is NOT observable.";
      }
      """;
    View_S3_3rdCodeSample.XamlCode = """
      <TextBlock Text="{Binding Main_S3_Text2}" />
      """;
    View_S3_3rdCodeSample.XamlHighlights = "Binding";
    View_S3_3rdCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        private string _main_S3_Text2 = "This object is observable.";
        public string Main_S3_Text2
        {
          get => _main_S3_Text2;
          set => SetProperty(ref _main_S3_Text2, value);
        }
      }
      """;
    View_S3_4thCodeSample.XamlCode = """
      <Border Style="{StaticResource View_BorderStyle}">
        <TextBlock Text="{Binding ElementName=View_S3_TextBox, Path=Text}" />
      </Border>
      """;
    View_S3_4thCodeSample.XamlHighlights = "Binding";
    View_S3_5thCodeSample.XamlCode = """
      <TextBlock Text="{Binding Main_S3_Array[2]}" />
      """;
    View_S3_5thCodeSample.XamlHighlights = "Binding";
    View_S3_5thCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        public string[] Main_S3_Array { get; } = 
        { 
          "Array value at index 0.",
          "Array value at index 1.",
          "Array value at index 2."
        };
      }
      """;
    View_S3_6thCodeSample.XamlCode = """
      <TextBlock Text="{Binding Main_S3_JaggedArray[1][2]}" />
      """;
    View_S3_6thCodeSample.XamlHighlights = "Binding";
    View_S3_6thCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        public string[][] Main_S3_JaggedArray { get; } =
        {
          new string[2] { "Array value at [0][0].", "Array value at [0][1]." },
          new string[3] { "Array value at [1][0].", "Array value at [1][1].", "Array value at [1][2]." },
          new string[1] { "Array value at [2][0]." },
        };
      }
      """;
    View_S3_7thCodeSample.XamlCode = """
      <TextBlock Text="{Binding Main_S3_List[2]}" />
      """;
    View_S3_7thCodeSample.XamlHighlights = "Binding";
    View_S3_7thCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        public List<string> Main_S3_List { get; } = new() 
        { 
          "List value at index 0.", 
          "List value at index 1.", 
          "List value at index 2.", 
          "List value at index 3." 
        };
      }
      """;
    View_S3_8thCodeSample.XamlCode = """
      <TextBlock Text="{Binding Main_S3_Dictionary[Third]}" />
      """;
    View_S3_8thCodeSample.XamlHighlights = "Binding";
    View_S3_8thCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        public Dictionary<string, string> Main_S3_Dictionary { get; } = new() 
        { 
          { "First", "Dictionary Value for Key 'First'." }, 
          { "Second", "Dictionary Value for Key 'Second'." }, 
          { "Third", "Dictionary Value for Key 'Third'." }, 
          { "Fourth", "Dictionary Value for Key 'Fourth'." }, 
        };
      }
      """;

    View_S3_9thCodeSample.XamlCode = """
      <StackPanel Spacing="30"
                  Orientation="Horizontal"
                  HorizontalAlignment="Center">
        <Slider Header="Position X"
                Minimum="0"
                Maximum="150"
                Width="250"
                Value="{Binding Main_S3_Dog.Position.X, Mode=TwoWay}" />
        <Slider Header="Position Y"
                Minimum="0"
                Maximum="150"
                Width="250"
                Value="{Binding Main_S3_Dog.Position.Y, Mode=TwoWay}" />
      </StackPanel>

      <Grid ColumnSpacing="24"
            HorizontalAlignment="Center">
        <Grid.ColumnDefinitions>
          <ColumnDefinition Width="Auto" />
          <ColumnDefinition Width="Auto" />
          <ColumnDefinition Width="Auto" />
        </Grid.ColumnDefinitions>

        <Canvas Grid.Column="0"
                Width="200"
                Height="200"
                Background="{StaticResource SolidBackgroundFillColorBase}">
          <TextBlock x:Name="View_S3_Dog"
                     Canvas.Left="{Binding Main_S3_Dog.Position.X}"
                     Canvas.Top="{Binding Main_S3_Dog.Position.Y}"
                     Text="&#x1F436;"
                     FontSize="32" />
        </Canvas>
        <StackPanel Grid.Column="1"
                    VerticalAlignment="Center"
                    Spacing="8">
          <TextBlock Text="Name: " />
          <TextBlock Text="Icon: " />
          <TextBlock Text="Position X: " />
          <TextBlock Text="Position Y: " />
        </StackPanel>
        <StackPanel Grid.Column="2"
                    VerticalAlignment="Center"
                    Spacing="8">
          <TextBlock Text="{Binding Main_S3_Dog.Name}" />
          <TextBlock Text="{Binding ElementName=View_S3_Dog, Path=Text}" />
          <TextBlock Text="{Binding ElementName=View_S3_Dog, Path=(Canvas.Left)}" />
          <TextBlock Text="{Binding ElementName=View_S3_Dog, Path=(Canvas.Top)}" />
        </StackPanel>
      </Grid>
      """;
    View_S3_9thCodeSample.XamlHighlights = "Binding";
    View_S3_9thCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
        public Dog Main_S3_Dog { get; } = new("Dog", true, 300);
      }
      """;
    #endregion

    #region Section 4
    View_S4_1stCodeSample.XamlCode = """
      <!-- OneTime -->
      <StackPanel Spacing="12"
                  Width="200">
        <TextBlock Text="OneTime" 
                   HorizontalAlignment="Center"/>
        <TextBox x:Name="View_S4_OneTimeSourceTextBox"
                 PlaceholderText="Enter text."
                 Text="OneTime" />
        <TextBox x:Name="View_S4_OneTimeTargetTextBox"
                 Text="{Binding ElementName=View_S4_OneTimeSourceTextBox, Path=Text, Mode=OneTime}" />
      </StackPanel>

      <!-- OneWay -->
      <StackPanel Spacing="12"
                  Width="200">
        <TextBlock Text="OneWay"
                   HorizontalAlignment="Center" />
        <TextBox x:Name="View_S4_OneWaySourceTextBox"
                 PlaceholderText="Enter text." />
        <TextBox x:Name="View_S4_OneWayTargetTextBox"
                 PlaceholderText="Mirrors above text."
                 Text="{Binding ElementName=View_S4_OneWaySourceTextBox, Path=Text, Mode=OneWay, UpdateSourceTrigger=PropertyChanged}" />
      </StackPanel>

      <!-- TwoWay -->
      <StackPanel Spacing="12"
                  Width="200">
        <TextBlock Text="TwoWay"
                   HorizontalAlignment="Center" />
        <TextBox x:Name="View_S4_TwoWaySourceTextBox"
                 PlaceholderText="Enter text."
                 Text="{Binding ElementName=View_S4_TwoWayTargetTextBox, Path=Text, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
        <TextBox x:Name="View_S4_TwoWayTargetTextBox"
                 PlaceholderText="Mirrors and edit above text."
                 Text="{Binding ElementName=View_S4_TwoWaySourceTextBox, Path=Text, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
      </StackPanel>
      """;
    View_S4_1stCodeSample.XamlHighlights = "Binding";
    #endregion

    #region Section 5
    View_S5_1stCodeSample.XamlCode = """
      <!-- LostFocus -->
      <StackPanel Spacing="12"
                  Width="200">
        <TextBlock Text="LostFocus"
                   HorizontalAlignment="Center" />
        <TextBox PlaceholderText="LostFocus"
                 Text="{Binding Main_S5_Text1, Mode=TwoWay, UpdateSourceTrigger=LostFocus}" />
        <Border Style="{StaticResource View_BorderStyle}">
          <TextBlock Text="{Binding Main_S5_Text1}" />
        </Border>
      </StackPanel>

      <!-- PropertyChanged -->
      <StackPanel Spacing="12"
                  Width="200">
        <TextBlock Text="PropertyChanged"
                   HorizontalAlignment="Center" />
        <TextBox PlaceholderText="PropertyChanged"
                 Text="{Binding Main_S5_Text2, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
        <Border Style="{StaticResource View_BorderStyle}">
          <TextBlock Text="{Binding Main_S5_Text2}" />
        </Border>
      </StackPanel>

      <!-- Explicit -->
      <StackPanel Spacing="12"
                  Width="200">
        <TextBlock Text="Explicit"
                   HorizontalAlignment="Center" />
        <TextBox x:Name="View_S5_ExplicitTextBox"
                 PlaceholderText="Explicit"
                 Text="{Binding Main_S5_Text3, Mode=TwoWay, UpdateSourceTrigger=Explicit}"
                 KeyUp="View_S5_ExplicitTextBox_KeyUp" />
        <Border Style="{StaticResource View_BorderStyle}">
          <TextBlock Text="{Binding Main_S5_Text3}" />
        </Border>
      </StackPanel>
      """;
    View_S5_1stCodeSample.XamlHighlights = "UpdateSourceTrigger";
    View_S5_1stCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
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
      }

      // BindingPage.xaml.cs
      public sealed partial class BindingPage : Page
      {
        private void View_S5_ExplicitTextBox_KeyUp(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
          if (e.Key == Windows.System.VirtualKey.Enter)
            View_S5_ExplicitTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
      }
      """;
    View_S5_1stCodeSample.CSharpHighlights = "GetBindingExpression||UpdateSource";
    #endregion

    #region Section 6
    View_S6_1stCodeSample.XamlCode = """
      <Page.Resources>
        <local:BoolToVisibilityConverter x:Key="BoolToVisibilityConverter" />
      </Page.Resources>

      <ToggleSwitch x:Name="View_S6_FirstToggleSwitch" />
      <TextBlock Text="Visible"
                 Visibility="{Binding ElementName=View_S6_FirstToggleSwitch, Path=IsOn, Converter={StaticResource BoolToVisibilityConverter}}" />
      """;
    View_S6_1stCodeSample.XamlHighlights = "Converter";
    View_S6_1stCodeSample.CSharpCode = """
      // Converter.cs
      public class BoolToVisibilityConverter : IValueConverter
      {
        public object Convert(object value, Type targetType, object parameter, string language)
          => value is bool boolValue
            ? boolValue ? Visibility.Visible : Visibility.Collapsed
            : throw new ArgumentException();

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
      }
      """;
    View_S6_2ndCodeSample.XamlCode = """
      <Page.Resources>
        <local:BoolNegationConverter x:Key="BoolNegationConverter" />
      </Page.Resources>

      <ToggleSwitch x:Name="View_S6_SecondToggleSwitch"
                    IsOn="{Binding ElementName=View_S6_FirstToggleSwitch, Path=IsOn, Mode=TwoWay, Converter={StaticResource BoolNegationConverter}}" />
      """;
    View_S6_2ndCodeSample.XamlHighlights = "Converter";
    View_S6_2ndCodeSample.CSharpCode = """
      // Converter.cs
      public class BoolNegationConverter : IValueConverter
      {
        public object Convert(object value, Type targetType, object parameter, string language) 
          => value is bool boolValue ? !boolValue : throw new ArgumentException();

        public object ConvertBack(object value, Type targetType, object parameter, string language)
          => value is bool boolValue ? !boolValue : throw new ArgumentException();
      }
      """;
    View_S6_3rdCodeSample.XamlCode = """
      <Page.Resources>
        <local:ColorToBrushConverter x:Key="ColorToBrushConverter" />
      </Page.Resources>

      <Rectangle Stretch="Fill"
                 Height="50"
                 RadiusX="24"
                 RadiusY="24"
                 Fill="{Binding ElementName=View_S6_ColorPicker, Path=Color, Mode=OneWay, Converter={StaticResource ColorToBrushConverter}}" />
      <ColorPicker x:Name="View_S6_ColorPicker"
                   Color="LightGoldenrodYellow"
                   ColorSpectrumShape="Ring"
                   IsMoreButtonVisible="True"
                   IsColorPreviewVisible="False"
                   HorizontalAlignment="Center" />
      """;
    View_S6_3rdCodeSample.XamlHighlights = "Converter";
    View_S6_3rdCodeSample.CSharpCode = """
      // Converter.cs
      public class ColorToBrushConverter : IValueConverter
      {
        public object Convert(object value, Type targetType, object parameter, string language) 
          => value is Color color ? new SolidColorBrush(color) : throw new ArgumentException();
        public object ConvertBack(object value, Type targetType, object parameter, string language)
          => value is SolidColorBrush brush ? brush.Color : throw new ArgumentException();
      }
      """;
    #endregion

    #region Section 7
    View_S7_1stCodeSample.XamlCode = """
      <Border Style="{StaticResource View_BorderStyle}"
              Width="400">
        <StackPanel Orientation="Horizontal"
                    Spacing="24">
          <StackPanel Spacing="8">
            <TextBlock Text="Object Type:" />
            <TextBlock Text="Name:" />
            <TextBlock Text="Gender:" />
            <TextBlock Text="Id:" />
            <TextBlock Text="Description:" />
          </StackPanel>

          <StackPanel Spacing="8">
            <TextBlock Text="{Binding Main_S7_Dog, FallbackValue='Cannot be resolved (FallbackValue)', TargetNullValue='Target is null (TargetNullValue)'}" />
            <TextBlock Text="{Binding Main_S7_Dog.Name, FallbackValue='Cannot be resolved (FallbackValue)', TargetNullValue='Target is null (TargetNullValue)'}" />
            <TextBlock Text="{Binding Main_S7_Dog.Gender, Converter={StaticResource BoolToGenderConverter}, FallbackValue='Cannot be resolved (FallbackValue)', TargetNullValue='Target is null (TargetNullValue)'}" />
            <TextBlock Text="{Binding Main_S7_Dog.Id, FallbackValue='Cannot be resolved (FallbackValue)', TargetNullValue='Target is null (TargetNullValue)'}" />
            <TextBlock Text="{Binding Main_S7_Dog.Description, FallbackValue='Cannot be resolved (FallbackValue)', TargetNullValue='Target is null (TargetNullValue)'}" />
          </StackPanel>
        </StackPanel>
      </Border>

      <StackPanel Spacing="8"
                  VerticalAlignment="Center">
        <Button x:Name="View_S7_CreateButton"
                Content="Create instance"
                IsEnabled="{Binding Main_S7_Dog, Converter={StaticResource ObjectNullConverter}}"
                Style="{StaticResource AccentButtonStyle}"
                Click="View_S7_CreateButton_Click" />
        <Button x:Name="View_S7_NullButton"
                Content="Make object null"
                IsEnabled="{Binding Main_S7_Dog, Converter={StaticResource ObjectNotNullConverter}}"
                Style="{StaticResource AccentButtonStyle}"
                Click="View_S7_NullButton_Click" />
        <AutoSuggestBox x:Name="View_S7_AutoSuggestBox"
                        PlaceholderText="Enter description."
                        QueryIcon="Forward"
                        Width="200"
                        IsEnabled="{Binding Main_S7_Dog, Converter={StaticResource ObjectNotNullConverter}}"
                        QuerySubmitted="View_S7_AutoSuggestBox_QuerySubmitted" />
      </StackPanel>
      """;
    View_S7_1stCodeSample.XamlHighlights = "Binding";
    View_S7_1stCodeSample.CSharpCode = """
      // BindingPage.xaml.cs
      public sealed partial class BindingPage : Page
      {
        private void View_S7_CreateButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
          ViewModel.Main_S7_Dog ??= new("Alex", true, 700);
        }

        private void View_S7_NullButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
          ViewModel.Main_S7_Dog = null;
          View_S7_AutoSuggestBox.Text = "";
        }

        private void View_S7_AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
          if (ViewModel.Main_S7_Dog is null)
            return;
          ViewModel.Main_S7_Dog.Description = string.IsNullOrEmpty(args.QueryText) ? null : args.QueryText;
        }
      }
      """;
    #endregion

    #region Section 8
    View_S8_1stCodeSample.XamlCode = """
      <ListView x:Name="View_S8_ListView"
                BorderBrush="{StaticResource SurfaceStrokeColorDefault}"
                BorderThickness="1"
                CornerRadius="{StaticResource ControlCornerRadius}">
        <ListView.Header>
          <StackPanel Orientation="Horizontal"
                      Spacing="8"
                      Padding="16,8"
                      BorderBrush="{StaticResource SurfaceStrokeColorDefault}"
                      BorderThickness="0,0,0,1">
            <TextBlock Text="Type"
                       Width="80"
                       Style="{StaticResource BodyStrongTextBlockStyle}" />
            <TextBlock Text="Name"
                       Width="100"
                       Style="{StaticResource BodyStrongTextBlockStyle}" />
            <TextBlock Text="Gender"
                       Width="100"
                       Style="{StaticResource BodyStrongTextBlockStyle}" />
            <TextBlock Text="Id"
                       Width="100"
                       Style="{StaticResource BodyStrongTextBlockStyle}" />
            <TextBlock Text="Description"
                       Style="{StaticResource BodyStrongTextBlockStyle}" />
          </StackPanel>
        </ListView.Header>

        <ListView.ItemTemplate>
          <DataTemplate>
            <StackPanel Orientation="Horizontal"
                        Spacing="8">
              <TextBlock Text="{Binding}"
                         Width="80" />
              <TextBlock Text="{Binding Name}"
                         Width="100" />
              <TextBlock Text="{Binding Gender, Converter={StaticResource BoolToGenderConverter}}"
                         Width="100" />
              <TextBlock Text="{Binding Id}"
                         Width="100" />
              <TextBlock Text="{Binding Description, TargetNullValue='No description'}" />
            </StackPanel>
          </DataTemplate>
        </ListView.ItemTemplate>
      </ListView>
      """;
    View_S8_1stCodeSample.XamlHighlights = "Binding";
    View_S8_1stCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
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
      }
      """;
    View_S8_2ndCodeSample.XamlCode = """
      <Border Grid.Column="0"
              Style="{StaticResource View_BorderStyle}">
        <RadioButtons SelectedIndex="0"
                      MaxColumns="1"
                      VerticalAlignment="Center">
          <RadioButtons.Header>
            <TextBlock Text="ItemsSource"
                       Style="{StaticResource BodyStrongTextBlockStyle}" />
          </RadioButtons.Header>
          <RadioButton x:Name="View_S8_DogsRadioButton"
                       Content="Dogs"
                       Checked="View_S8_DogsRadioButton_Checked" />
          <RadioButton x:Name="View_S8_CatsRadioButton"
                       Content="Cats"
                       Checked="View_S8_CatsRadioButton_Checked" />
        </RadioButtons>

      </Border>

      <Border Grid.Column="1"
              Style="{StaticResource View_BorderStyle}">
        <StackPanel Spacing="8"
                    VerticalAlignment="Center">
          <TextBlock Text="Items"
                     Style="{StaticResource BodyStrongTextBlockStyle}" />
          <Button x:Name="View_S8_AddButton"
                  Content="Add a new item"
                  Background="{StaticResource AcrylicBackgroundFillColorBaseBrush}"
                  Click="View_S8_AddButton_Click" />
          <Button x:Name="View_S8_DeleteButton"
                  Content="Delete selected item"
                  IsEnabled="{Binding ElementName=View_S8_ListView, Path=SelectedItem, Converter={StaticResource ObjectNotNullConverter}}"
                  Background="{StaticResource AcrylicBackgroundFillColorBaseBrush}"
                  Click="View_S8_DeleteButton_Click" />
        </StackPanel>
      </Border>

      <Border Grid.Column="2"
              Style="{StaticResource View_BorderStyle}">
        <TextBox Text="{Binding ElementName=View_S8_ListView, Path=SelectedItem.Description, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
                 IsEnabled="{Binding ElementName=View_S8_ListView, Path=SelectedItem, Converter={StaticResource ObjectNotNullConverter}}">
          <TextBox.Header>
            <TextBlock Text="Edit description"
                       Style="{StaticResource BodyStrongTextBlockStyle}" />
          </TextBox.Header>
        </TextBox>
      </Border>
      """;
    View_S8_2ndCodeSample.XamlHighlights = "Binding";
    View_S8_2ndCodeSample.CSharpCode = """
      // MainViewModel.cs
      public class MainViewModel : ObservableObject
      {
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
      }

      // BindingPage.xaml.cs
      public sealed partial class BindingPage : Page
      {
        private void View_S8_DogsRadioButton_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
          View_S8_ListView.ItemsSource = ViewModel.Main_S8_Dogs;
        }

        private void View_S8_CatsRadioButton_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
          View_S8_ListView.ItemsSource = ViewModel.Main_S8_Cats;
        }

        private void View_S8_AddButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
          int index = new Random().Next(100);
          if (View_S8_DogsRadioButton.IsChecked == true)
            ViewModel.Main_S8_Dogs.Add(new Dog($"Dog {index}", index % 2 == 0, 800 + index));
          else if (View_S8_CatsRadioButton.IsChecked == true)
            ViewModel.Main_S8_Cats.Add(new Cat($"Cat {index}", index % 2 == 0, $"A-{800 + index}"));
        }

        private void View_S8_DeleteButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
          if (View_S8_ListView.SelectedItem is Animal animal)
          {
            if (View_S8_DogsRadioButton.IsChecked == true)
              ViewModel.Main_S8_Dogs.Remove((Dog)animal);
            else if (View_S8_CatsRadioButton.IsChecked == true)
              ViewModel.Main_S8_Cats.Remove((Cat)animal);
          }
        }
      }
      """;
    #endregion

    #region Section 9
    View_S9_1stCodeSample.XamlCode = """
      <ToggleButton Content="Click"
                    Background="{StaticResource AcrylicBackgroundFillColorBaseBrush}">
        <ToggleButton.Template>
          <ControlTemplate TargetType="ToggleButton">
            <Grid x:Name="RootGrid"
                  Background="{TemplateBinding Background}"
                  BorderBrush="{TemplateBinding BorderBrush}"
                  BorderThickness="{TemplateBinding BorderThickness}"
                  CornerRadius="{TemplateBinding CornerRadius}"
                  Padding="{TemplateBinding Padding}">
              <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto" />
                <ColumnDefinition />
              </Grid.ColumnDefinitions>

              <!-- 토글 상태 표시 테두리 -->
              <Ellipse Grid.Column="0"
                       Width="8"
                       Height="8"
                       Margin="0,0,6,0"
                       Stroke="{StaticResource ControlStrongStrokeColorDefault}"
                       StrokeThickness="1" />

              <!-- 토글 상태 표시 내부 (토글이 켜지면 색상이 채워짐)  -->
              <Ellipse Grid.Column="0"
                       Width="8"
                       Height="8"
                       Margin="0,0,6,0"
                       Fill="{StaticResource AccentFillColorDefaultBrush}"
                       Visibility="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=IsChecked, Converter={StaticResource BoolToVisibilityConverter}}" />

              <!-- 콘텐츠 표시 부분 -->
              <ContentPresenter Grid.Column="1"
                                Content="{TemplateBinding Content}" />
            </Grid>
          </ControlTemplate>
        </ToggleButton.Template>
      </ToggleButton>
      """;
    View_S9_1stCodeSample.XamlHighlights = "RelativeSource";
    #endregion
  }
}
