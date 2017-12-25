Param( 
	[string]
	$target="Debug",
	
	[Parameter(Mandatory=$true)]
	[string]
	$releaseNotes
)

$proyectName = "BackEnd.Client"

$exists = Test-Path C:\Repos\Tools\Nuget\nuget.exe

if ($exists -eq $false) {
    "Descargando NuGet.Exe"
    $source = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
    Invoke-WebRequest $source -OutFile ..\Tools\nuget.exe
    "Descarga Completa"
}

<# ..\Tools\nuget.exe update -self #>

[array]$fileList = gci -Recurse "$proyectName.csproj"

if ($fileList.Count -eq 0) {
    "No se encontró ningún proyecto con ese nombre"
    return
}

if ([string]::IsNullOrEmpty($target)){
	$target = "Debug"
} 

$username = ([adsi]"WinNT://$env:userdomain/$env:username,user").fullname
$properties = "CreatedBy=$username;Configuration=$target;Platform=AnyCPU;releaseNotes=$releaseNotes"
$outputDir = "C:\Repos\CustomPkg\"

"Properties: $properties"

foreach ($item in $fileList) {
    [string]$fileName = $item.fullName
    [string]$actualProy = $item.name.Replace(".csproj", "")
    "Empaquetando $actualProy"

    C:\Repos\Tools\Nuget\nuget.exe pack -build "$fileName" -properties $properties -Output "C:\Repos\CustomPkg\"
}
