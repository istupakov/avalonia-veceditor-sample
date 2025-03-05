using System.Composition;
using System.Text.Json.Serialization;

using CommunityToolkit.Mvvm.ComponentModel;

namespace VecEditor.Models;

[Export(typeof(IGeometryModel))]
[ExportMetadata("Name", "Circle")]
public partial class CircleModel : ObservableObject, IGeometryModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _centerX;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _centerY;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _radius = 10;

    [JsonIgnore]
    public string Geometry => FormattableString.Invariant($"""
                                                           M{CenterX + Radius},{CenterY}
                                                           A{Radius},{Radius},0,1,1,{CenterX - Radius},{CenterY}
                                                           A{Radius},{Radius},0,1,1,{CenterX + Radius},{CenterY} z
                                                           """);

    public void Scale(float ratio)
    {
        Radius *= ratio;
    }
}
