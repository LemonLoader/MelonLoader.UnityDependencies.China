using _U = AssetRipper.Primitives.UnityVersion;

namespace Generator;

public struct UnityVersion
{
    public required _U Primitive {  get; set; }

    public required int Major { get; set; }
    public required int Minor { get; set; }
    public required int Patch { get; set; }
    public required int BuildNumber { get; set; }
    public required string Id { get; set; }

    public string ShortName => Primitive.ToStringWithoutType();

    public readonly override string ToString()
         => Primitive.ToString();

    public static bool TryParse(string value, string id, out UnityVersion unityVersion)
    {
        unityVersion = default;

        if (!_U.TryParse(value, out _U p, out string? customEngine))
            return false;

        unityVersion = new()
        {
            Major = p.Major,
            Minor = p.Minor,
            Patch = p.Build,
            BuildNumber = p.TypeNumber,
            Primitive = p,
            Id = id
        };

        return true;
    }
}