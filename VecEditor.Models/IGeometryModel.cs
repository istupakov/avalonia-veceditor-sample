using System.Text.Json.Serialization;

namespace VecEditor.Models;

[JsonDerivedType(typeof(CircleModel), typeDiscriminator: "circle")]
[JsonDerivedType(typeof(RectangleModel), typeDiscriminator: "rectangle")]
public interface IGeometryModel
{
    float CenterX { get; set; }
    float CenterY { get; set; }
    string Geometry { get; }

    void Scale(float ratio);
}
