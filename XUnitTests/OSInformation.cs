using System;
using System.Runtime.InteropServices;

using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

public class RunningOnOS
{
    private readonly ITestOutputHelper output;

    public RunningOnOS(ITestOutputHelper output)
    {
        this.output = output;
    }

    private bool InWindows { get { return Environment.OSVersion.VersionString.Contains("Win"); } }

    // Build container before test run from Developer powershell (Ctrl+`):
    // docker build -t testenv -f testenv.Dockerfile .
    [SkippableFact]
    public void IsRunningOnLinux()
    {
        Skip.If(InWindows, "Linux test, skip if in Windows environment");
        Assert.True(RuntimeInformation.IsOSPlatform(OSPlatform.Linux));
    }

    [SkippableFact]
    public void IsRunningOnWindows()
    {
        Skip.IfNot(InWindows, "Windows test, skip if in linux environment");
        Assert.True(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
    }

    [SkippableFact]
    public void WindowsVersionTest()
    {
        Skip.IfNot(InWindows, "Windows test, skip if in linux environment");
        Assert.True(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
        output.WriteLine(Environment.OSVersion.VersionString);

    }

}
