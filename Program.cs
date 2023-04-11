using System.Diagnostics;
using System.Runtime.InteropServices;
string perfCounters = string.Empty;
if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    throw new NotImplementedException("OSX not supported yet");
var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
var task = Task.Factory.StartNew(() =>
{
    var proc = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = isLinux ? "vmstat" : "typeperf.exe",
            Arguments = isLinux ? "1" : "-cf counters.txt",
            RedirectStandardOutput = true,
            CreateNoWindow = true
        }
    };
    proc.Start();
    while (!proc.StandardOutput.EndOfStream)
    {
        var line = proc.StandardOutput.ReadLine();
        if (string.IsNullOrEmpty(line) || line.Contains("s")) continue;
        try
        {
            string[] parts = isLinux ?
                line.Split(' ', StringSplitOptions.RemoveEmptyEntries) :
                line.Split(',').Skip(1).Select(o => o.Trim('"')).ToArray();
            perfCounters = $"{DateTime.Now:HH:mm:ss.fff},{string.Join(",", parts)}";
        } catch { /* ignore */ }
    }
});
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => perfCounters);
app.Run();