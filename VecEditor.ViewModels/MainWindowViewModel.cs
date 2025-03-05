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
    public IEnumerable<ExportFactory<IGeometryModel, ModelMetadata>> ModelFactories { get; set; } = [];

    [Import]
    public GeometryJsonSerializer? GeometryJsonSerializer { get; set; }

    public MainWindowViewModel()
    {
        var configuration = new ContainerConfiguration()
            .WithAssembly(typeof(IGeometryModel).Assembly)
            .WithAssembly(typeof(GeometryJsonSerializer).Assembly);
        using var container = configuration.CreateContainer();
        container.SatisfyImports(this);

        foreach (var factory in ModelFactories)
        {
            Factories.Add(new() { Factory = factory, Main = this });
        }

        SaveJsonCommand = new RelayCommand(
            () => { GeometryJsonSerializer?.SaveJson(JsonFilename, Figures); LoadJsonCommand?.NotifyCanExecuteChanged(); });
        LoadJsonCommand = new RelayCommand(
            () => LoadFigures(GeometryJsonSerializer?.LoadJson<GeometryViewModel>(JsonFilename)),
            () => File.Exists(JsonFilename));
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
