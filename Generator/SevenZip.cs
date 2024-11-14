using System.Diagnostics;

namespace Generator;

public static class SevenZip
{
    public static async Task ExtractAsync(string archive, string outputDir, bool keepStructure, params string[] fileFilter)
    {
        var args = new List<string>()
        {
            keepStructure ? "x" : "e", archive, "-y", $"-o{outputDir}"
        };
        
        args.AddRange(fileFilter);
        
        var proc = Process.Start("7z", args);
        await proc.WaitForExitAsync();
        if (proc.ExitCode != 0)
            throw new($"7z exit code: {proc.ExitCode}");
    }
}