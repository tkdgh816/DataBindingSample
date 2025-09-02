using System;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

using Windows.UI;

namespace DataBindingSample;

public class BoolToVisibilityConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, string language)
    => value is bool boolValue
      ? boolValue ? Visibility.Visible : Visibility.Collapsed
      : throw new ArgumentException();

  public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}

public class BoolNegationConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, string language)
    => value is bool boolValue ? !boolValue : throw new ArgumentException();

  public object ConvertBack(object value, Type targetType, object parameter, string language)
    => value is bool boolValue ? !boolValue : throw new ArgumentException();
}

public class ColorToBrushConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, string language)
    => value is Color color ? new SolidColorBrush(color) : throw new ArgumentException();
  public object ConvertBack(object value, Type targetType, object parameter, string language)
    => value is SolidColorBrush brush ? brush.Color : throw new ArgumentException();
}

public class BoolToGenderConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, string language)
    => value is bool boolValue
    ? boolValue ? "Male" : "Female"
    : throw new ArgumentException();

  public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}

public class ObjectNullConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, string language) => value is null;
  public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}

public class ObjectNotNullConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, string language) => value is not null;
  public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}