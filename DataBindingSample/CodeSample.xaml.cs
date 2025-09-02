using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;

namespace DataBindingSample;

public sealed partial class CodeSample : UserControl
{
  public CodeSample()
  {
    InitializeComponent();
    Loaded += CodeSample_Loaded;
  }

  private void CodeSample_Loaded(object sender, RoutedEventArgs e)
  {
    if (View_XamlCode_SelectorBarItem.IsEnabled)
      View_SelectorBar.SelectedItem = View_XamlCode_SelectorBarItem;
    else if (View_CSharpCode_SelectorBarItem.IsEnabled)
      View_SelectorBar.SelectedItem = View_CSharpCode_SelectorBarItem;
  }

  public static readonly DependencyProperty XamlCodeProperty = DependencyProperty.Register("XamlCode", typeof(string), typeof(CodeSample), new PropertyMetadata(null));
  public string XamlCode
  {
    get => (string)GetValue(XamlCodeProperty);
    set => SetValue(XamlCodeProperty, value);
  }

  public static readonly DependencyProperty XamlHighlightsProperty = DependencyProperty.Register("XamlHighlights", typeof(string), typeof(CodeSample), new PropertyMetadata(""));
  public string XamlHighlights
  {
    get => (string)GetValue(XamlHighlightsProperty);
    set => SetValue(XamlHighlightsProperty, value);
  }

  public static readonly DependencyProperty CSharpCodeProperty = DependencyProperty.Register("CSharpCode", typeof(string), typeof(CodeSample), new PropertyMetadata(null));
  public string CSharpCode
  {
    get => (string)GetValue(CSharpCodeProperty);
    set => SetValue(CSharpCodeProperty, value);
  }

  public static readonly DependencyProperty CSharpHighlightsProperty = DependencyProperty.Register("CSharpHighlights", typeof(string), typeof(CodeSample), new PropertyMetadata(""));
  public string CSharpHighlights
  {
    get => (string)GetValue(CSharpHighlightsProperty);
    set => SetValue(CSharpHighlightsProperty, value);
  }

  private void View_SelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
  {
    View_TextBlock.Inlines.Clear();
    if (sender.SelectedItem == View_XamlCode_SelectorBarItem)
      foreach (var inline in XamlToInlines(XamlCode, XamlHighlights.Split("||", System.StringSplitOptions.RemoveEmptyEntries)))
        View_TextBlock.Inlines.Add(inline);
    else if (sender.SelectedItem == View_CSharpCode_SelectorBarItem)
      foreach (var inline in CSharpToInlines(CSharpCode, CSharpHighlights.Split("||", System.StringSplitOptions.RemoveEmptyEntries)))
        View_TextBlock.Inlines.Add(inline);
  }

  private readonly SolidColorBrush _colorBlue = new(Colors.Blue);
  private readonly SolidColorBrush _colorCyan = new(Colors.DarkCyan);
  private readonly SolidColorBrush _colorBrown = new(Colors.Brown);
  private readonly SolidColorBrush _colorBlack = new(Colors.Black);
  private readonly SolidColorBrush _colorGreen = new(Colors.Green);

  private readonly Regex _xamlRegex = new(@"(\S*=)|("".*"")|(<!.*>)");

  private List<Inline> XamlToInlines(string? xamlText, params string[] boldWords)
  {
    List<Inline> inlines = new();
    if (xamlText is not null)
    {
      int lastIndex = 0;

      foreach (Match match in _xamlRegex.Matches(xamlText))
      {
        if (lastIndex < match.Index)
          inlines.Add(new Run() { Text = xamlText[lastIndex..match.Index], Foreground = _colorBlue });

        bool bold = boldWords is not null && boldWords.Length > 0 && boldWords.Any(match.Value.Contains);
        inlines.Add(new Run()
        {
          Text = match.Value,
          Foreground = match.Value switch
          {
            string v when v.StartsWith('"') => _colorCyan,
            string v when v.EndsWith('=') => _colorBrown,
            string v when v.StartsWith("<!") => _colorGreen,
            _ => _colorBlue
          },
          FontWeight = bold ? FontWeights.Normal : FontWeights.SemiLight
        });

        lastIndex = match.Index + match.Length;
      }

      if (lastIndex < xamlText.Length)
        inlines.Add(new Run() { Text = xamlText[lastIndex..], Foreground = _colorBlue });
    }

    return inlines;
  }

  private List<Inline> CSharpToInlines(string? csharpText, params string[] boldWords)
  {
    List<Inline> inlines = new();

    if (csharpText is not null)
    {
      SyntaxTree tree = CSharpSyntaxTree.ParseText(csharpText);
      var root = tree.GetRoot();
      foreach (var token in root.DescendantTokens())
      {
        foreach (var trivia in token.LeadingTrivia)
          inlines.Add(new Run() { Text = trivia.ToString(), Foreground = _colorGreen });

        bool bold = boldWords is not null && boldWords.Length > 0 && boldWords.Any(token.Text.Contains);

        var run = new Run()
        {
          Text = token.Text,
          Foreground = token switch
          {
            SyntaxToken t when t.IsKeyword() => _colorBlue,
            SyntaxToken t when t.IsKind(SyntaxKind.StringLiteralToken) => _colorBrown,
            _ => _colorBlack
          },
          FontWeight = bold ? FontWeights.Normal : FontWeights.SemiLight
        };

        inlines.Add(run);

        foreach (var trivia in token.TrailingTrivia)
          inlines.Add(new Run() { Text = trivia.ToString(), Foreground = _colorGreen });
      }
    }
    return inlines;
  }
}
