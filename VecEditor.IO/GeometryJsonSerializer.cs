using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

using System.Composition;

namespace VecEditor.IO;

[Export]
public class GeometryJsonSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    [ImportingConstructor]
    public GeometryJsonSerializer([ImportMany] IJsonTypeInfoResolver[] resolvers)
    {
        _jsonSerializerOptions = new()
        {
            WriteIndented = true,
            TypeInfoResolver = JsonTypeInfoResolver.Combine(resolvers)
        };
    }

    public void SaveJson<T>(string filename, IEnumerable<T> figures)
    {
        File.WriteAllText(filename, JsonSerializer.Serialize(figures, _jsonSerializerOptions));
    }

    public IEnumerable<T>? LoadJson<T>(string filename)
    {
        if (!File.Exists(filename))
            return null;
        return JsonSerializer.Deserialize<IEnumerable<T>>(File.ReadAllText(filename), _jsonSerializerOptions);
    }
}
