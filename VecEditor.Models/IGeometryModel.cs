namespace VecEditor.Models;

public interface IGeometryModel
{
    float CenterX { get; set; }
    float CenterY { get; set; }
    string Geometry { get; }

    void Scale(float ratio);
}
