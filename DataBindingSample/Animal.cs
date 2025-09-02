using CommunityToolkit.Mvvm.ComponentModel;

namespace DataBindingSample;

public class Animal : ObservableObject
{
  public string Name { get; }

  public bool Gender { get; }

  private string? _description;
  public string? Description
  {
    get => _description;
    set => SetProperty(ref _description, value);
  }

  private Position _position = new() { X = 0.0, Y = 0.0 };
  public Position Position
  {
    get => _position;
    set => SetProperty(ref _position, value);
  }

  public Animal(string name, bool gender)
  {
    Name = name;
    Gender = gender;
  }

  public override string ToString() => GetType().Name;
}

public class Dog : Animal
{
  public int Id { get; }

  public Dog(string name, bool gender, int id) : base(name, gender)
  {
    Id = id;
  }
}

public class Cat : Animal
{
  public string Id { get; }

  public Cat(string name, bool gender, string id) : base(name, gender)
  {
    Id = id;
  }
}

public class Position : ObservableObject
{
  private double _x;
  public double X
  {
    get => _x;
    set => SetProperty(ref _x, value);
  }

  private double _y;
  public double Y
  {
    get => _y;
    set => SetProperty(ref _y, value);
  }
}