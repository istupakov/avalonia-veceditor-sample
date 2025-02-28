using System.Text.Json.Serialization;
using System.Windows.Input;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using VecEditor.Models;

namespace VecEditor.ViewModels;

public partial class GeometryViewModel : ViewModelBase
{
    [ObservableProperty]
    [property: JsonIgnore]
    private MainWindowViewModel? _main;

    [ObservableProperty]
    private string _name = "Name";

    [ObservableProperty]
    [property: JsonIgnore]
    private Color _color;

    [JsonPropertyName("Color")]
    public string ColorStr
    {
        get => Color.ToString();
        set => Color = Color.Parse(value);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Thickness))]
    [property: JsonIgnore]
    private bool _isSelected;

    [JsonIgnore]
    public float Thickness => IsSelected ? 2 : 1;

    public required IGeometryModel Model { get; init; }

    [JsonIgnore]
    public ICommand ScaleUp { get; }
    [JsonIgnore]
    public ICommand ScaleDown { get; }
    [JsonIgnore]
    public ICommand Remove { get; }

    public GeometryViewModel()
    {
        ScaleUp = new RelayCommand(() => Model?.Scale(2));
        ScaleDown = new RelayCommand(() => Model?.Scale(0.5f));
        Remove = new RelayCommand(() => Main?.Figures.Remove(this));
    }
}
