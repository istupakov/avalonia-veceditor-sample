using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

using System.Composition;
using VecEditor.Models;

namespace VecEditor.IO;

[Export(typeof(IJsonTypeInfoResolver))]
public class GeometryModelTypeResolver : DefaultJsonTypeInfoResolver
{
    [ImportMany]
    public IEnumerable<Lazy<IGeometryModel, ModelMetadata>> Models { get; set; } = [];

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        if (jsonTypeInfo.Type == typeof(IGeometryModel))
        {
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions();
            foreach (var model in Models)
            {
                jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new(model.Value.GetType(), model.Metadata.Name));
            }
        }

        if (jsonTypeInfo.Type.IsAssignableTo(typeof(IGeometryModel)))
        {
            var geometryProp = jsonTypeInfo.Properties.First(p => p.Name == nameof(IGeometryModel.Geometry));
            jsonTypeInfo.Properties.Remove(geometryProp);
        }

        return jsonTypeInfo;
    }
}
