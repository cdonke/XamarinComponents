var TARGET = Argument("t", Argument("target", "ci"));

var SRC_COMMIT = "b3ce14af8949829463f07c667125fca024661f36";
var SRC_URL = $"https://github.com/xamarin/xamarin.exposurenotification/archive/{SRC_COMMIT}.zip";

var OUTPUT_PATH = (DirectoryPath)"./output/";
var NUGET_VERSION = "0.1.0-beta1";

Task("externals")
	.Does(() =>
{
	DownloadFile(SRC_URL, "./src.zip");

	Unzip("./src.zip", "./");

	MoveDirectory($"./xamarin.exposurenotification-{SRC_COMMIT}", "./src");
});

Task("libs")
	.IsDependentOn("externals")
	.Does(() =>
{
	EnsureDirectoryExists(OUTPUT_PATH);

	MSBuild($"./src/Xamarin.ExposureNotification/Xamarin.ExposureNotification.csproj", c => c
		.SetConfiguration("Release")
		.WithRestore()
		.WithTarget("Build")
		.WithTarget("Pack")
		.WithProperty("PackageVersion", NUGET_VERSION)
		.WithProperty("PackageOutputPath", MakeAbsolute(OUTPUT_PATH).FullPath));
});

Task("nuget")
	.IsDependentOn("libs");

Task("samples")
	.IsDependentOn("nuget");

Task("clean")
	.Does(() =>
{
	CleanDirectories("./externals/");
});


Task("ci")
	.IsDependentOn("nuget");

RunTarget(TARGET);
