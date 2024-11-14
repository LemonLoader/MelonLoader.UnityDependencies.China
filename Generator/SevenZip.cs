using System.Diagnostics;

namespace Generator;

public static class SevenZip
{
    private static readonly string sevenZipPath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, "Dependencies", "7zz");
    
    public static async Task ExtractAsync(string archive, string outputDir, bool keepStructure, params string[] fileFilter)
    {
        var args = new List<string>()
        {
            keepStructure ? "x" : "e", archive, "-y", $"-o{outputDir}"
        };
        
        args.AddRange(fileFilter);
        
        var proc = Process.Start(new ProcessStartInfo(sevenZipPath, args)
        {
            RedirectStandardOutput = true
        })!;
        await proc.WaitForExitAsync();
        if (proc.ExitCode != 0)
            throw new($"7z exit code: {proc.ExitCode}");
    }
}