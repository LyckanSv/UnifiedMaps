#tool nuget:?package=GitVersion.CommandLine
#tool "nuget:?package=NUnit.ConsoleRunner"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

string nugetVersion = null;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(Directory("./bin"));
    CleanDirectory(Directory("./obj"));
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./UnifiedMaps.sln");
});

Task("UpdateAssemblyInfo")
    .Does(() =>
{
    // Update the CI version
    GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = false,
        OutputType = GitVersionOutput.BuildServer
    });

    // Update the assembly versions
    var versionInfo = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });

    nugetVersion = versionInfo.NuGetVersion;

    Information("Version: {0}",  versionInfo.FullSemVer);
    Information("NuGet Version: {0}", nugetVersion);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./UnifiedMaps.sln", settings => {
        settings.SetConfiguration(configuration);
        settings.MSBuildPlatform = Cake.Common.Tools.MSBuild.MSBuildPlatform.x86;
      });
    }
    else
    {
      // Use XBuild
      XBuild("./UnifiedMaps.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .Does(() =>
{
    NUnit3("./bin/Tests/*.Tests.dll", new NUnit3Settings {
        NoResults = true
    });
});

Task("NuGet-Pack")
    .Does( () =>
{
    Func<string,string> replacer = (s) => s;
    if (IsRunningOnWindows()) {
        replacer = (s) => s.Replace("/", @"\");
    }

    var nuGetPackSettings = new NuGetPackSettings {
        Id                      = "UnifiedMaps",
        Version                 = nugetVersion,
        Copyright               = "fivenine GmbH " + DateTime.Now.Year,
        BasePath                = "./bin",
        Files                   = new [] {
            // netstandard2.0
            new NuSpecContent {Source = replacer("pcl/netstandard2.0/UnifiedMap.dll"), Target = replacer("lib/netstandard2.0/")},
            new NuSpecContent {Source = replacer("pcl/netstandard2.0/UnifiedMap.xml"), Target = replacer("lib/netstandard2.0/")},

            // Xamarin.iOS Unified API
            new NuSpecContent {Source = replacer("Xamarin.iOS10/UnifiedMap*.dll"), Target = replacer("lib/Xamarin.iOS10/")},
            new NuSpecContent {Source = replacer("Xamarin.iOS10/UnifiedMap*.xml"), Target = replacer("lib/Xamarin.iOS10/")},

            // Xamarin.Mac Unified API
            new NuSpecContent {Source = replacer("Xamarin.iOS10/UnifiedMap*.dll"), Target = replacer("lib/Xamarin.Mac20/")},
            new NuSpecContent {Source = replacer("Xamarin.iOS10/UnifiedMap*.xml"), Target = replacer("lib/Xamarin.Mac20/")},

            // Xamarin Android
            new NuSpecContent {Source = replacer("monoandroid/UnifiedMap*.dll"), Target = replacer("lib/MonoAndroid10/")},
            new NuSpecContent {Source = replacer("monoandroid/UnifiedMap*.xml"), Target = replacer("lib/MonoAndroid10/")},
        }
    };

    NuGetPack("./UnifiedMaps.nuspec", nuGetPackSettings);

    if (AppVeyor.IsRunningOnAppVeyor)
    {
        foreach (var file in GetFiles("UnifiedMaps*.nupkg"))
            AppVeyor.UploadArtifact(file.FullPath);
    }
});

Task("NuGet-Publish")
    .Does( () =>
{
    NuGetPush(GetFiles("UnifiedMaps*.nupkg").First(), new NuGetPushSettings {
        Source = "https://www.nuget.org/api/v2/package",
        ApiKey = EnvironmentVariable("NUGET_API_KEY")
    });
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

Task("Build-CI")
    .IsDependentOn("UpdateAssemblyInfo")
    .IsDependentOn("Build");

Task("Build-CI-AppVeyor")
    .IsDependentOn("UpdateAssemblyInfo")
    .IsDependentOn("Build")
    .IsDependentOn("NuGet-Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);