using System.Collections.ObjectModel;
using System.Composition;
using System.Composition.Hosting;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

using VecEditor.IO;
using VecEditor.Models;

namespace VecEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<GeometryViewModel> Figures { get; } = [];

    public ObservableCollection<ModelFactoryViewModel> Factories { get; } = [];

    public const string JsonFilename = "figures.json";
    public ICommand SaveJsonCommand { get; }
    public RelayCommand LoadJsonCommand { get; }

    [ImportMany]
    private IEnumerable<ExportFactory<IGeometryModel, ModelMetadata>> ModelFactories { get; set; } = [];

    private readonly GeometryJsonSerializer<GeometryViewModel> _geometryJsonSerializer = new();

    public MainWindowViewModel()
    {
        var configuration = new ContainerConfiguration()
            .WithAssembly(typeof(IGeometryModel).Assembly);
        using var container = configuration.CreateContainer();
        container.SatisfyImports(this);

        foreach (var factory in ModelFactories)
        {
            Factories.Add(new() { Factory = factory, Main = this });
        }

        SaveJsonCommand = new RelayCommand(() => { _geometryJsonSerializer.SaveJson(JsonFilename, Figures); LoadJsonCommand?.NotifyCanExecuteChanged(); });
        LoadJsonCommand = new RelayCommand(() => LoadFigures(_geometryJsonSerializer.LoadJson(JsonFilename)), () => File.Exists(JsonFilename));
    }

    private void LoadFigures(IEnumerable<GeometryViewModel>? figures)
    {
        if (figures is null)
            return;

        Figures.Clear();
        foreach (var fig in figures)
        {
            fig.Main = this;
            Figures.Add(fig);
        }
    }
}
