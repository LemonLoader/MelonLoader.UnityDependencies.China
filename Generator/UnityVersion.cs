namespace Generator;

public struct UnityVersion
{
    public required int Major { get; set; }
    public required int Minor { get; set; }
    public required int Patch { get; set; }
    public required char BuildType { get; set; }
    public required int BuildNumber { get; set; }
    public required string Id { get; set; }
    
    public string ShortName => $"{Major}.{Minor}.{Patch}";

    public readonly override string ToString()
        => $"{Major}.{Minor}.{Patch}{BuildType}{BuildNumber}";

    public static bool TryParse(string value, string id, out UnityVersion unityVersion)
    {
        unityVersion = default;
        
        var dot1 = value.IndexOf('.');
        if (dot1 == -1 || !int.TryParse(value.AsSpan(0, dot1), out var major))
            return false;
        
        var dot2 = value.IndexOf('.', dot1 + 1);
        if (dot2 == -1 || !int.TryParse(value.AsSpan(dot1 + 1, dot2 - dot1 - 1), out var minor))
            return false;

        var i = dot2;
        while (true)
        {
            i++;
            
            if (i >= value.Length)
                return false;
            
            if (value[i] < '0' || value[i] > '9')
                break;
        }

        if (!int.TryParse(value.AsSpan(dot2 + 1, i - dot2 - 1), out var patch))
            return false;

        if (!int.TryParse(value.AsSpan(i + 1, value.Length - i - 1), out var buildNumber))
            return false;

        unityVersion = new()
        {
            Major = major,
            Minor = minor,
            Patch = patch,
            BuildType = value[i],
            BuildNumber = buildNumber,
            Id = id
        };

        return true;
    }
}