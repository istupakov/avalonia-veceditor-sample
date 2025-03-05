using System.Composition;

using CommunityToolkit.Mvvm.ComponentModel;

namespace VecEditor.Models;

[Export(typeof(IGeometryModel))]
[ExportMetadata("Name", "Rectangle")]
public partial class RectangleModel : ObservableObject, IGeometryModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _centerX;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _centerY;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _width = 10;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _height = 10;

    public string Geometry => FormattableString.Invariant($"""
                                                           M{CenterX - Width / 2},{CenterY - Height / 2}
                                                           h{Width} v{Height} h{-Width} z
                                                           """);

    public void Scale(float ratio)
    {
        Width *= ratio;
        Height *= ratio;
    }
}
