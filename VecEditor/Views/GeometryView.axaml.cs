using Avalonia.Controls;
using Avalonia.Interactivity;

using VecEditor.ViewModels;

namespace VecEditor.Views;

public partial class GeometryView : UserControl
{
    public GeometryView()
    {
        InitializeComponent();
    }

    void Path_PointerPressed(object sender, RoutedEventArgs e)
    {
        if (DataContext is GeometryViewModel model)
        {
            model.IsSelected = true;
        }
    }
}
