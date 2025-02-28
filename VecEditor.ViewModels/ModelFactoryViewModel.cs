using System.Composition;
using System.Windows.Input;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using VecEditor.Models;

namespace VecEditor.ViewModels;

public partial class ModelFactoryViewModel : ViewModelBase
{
    [ObservableProperty]
    private MainWindowViewModel? _main;
    public required ExportFactory<IGeometryModel, ModelMetadata> Factory { get; init; }
    public ICommand CreateCommand { get; }

    public ModelFactoryViewModel()
    {
        CreateCommand = new RelayCommand(() => Main?.Figures.Add(Create()));
    }

    private GeometryViewModel Create()
    {
        var color = Color.FromRgb((byte)Random.Shared.Next(256), (byte)Random.Shared.Next(256), (byte)Random.Shared.Next(256));
        var name = $"{Factory.Metadata.Name} {Random.Shared.Next(100)}";
        var model = Factory.CreateExport().Value;
        model.CenterX = Random.Shared.Next(256);
        model.CenterY = Random.Shared.Next(256);
        return new() { Name = name, Color = color, Model = model, Main = Main };
    }
}
