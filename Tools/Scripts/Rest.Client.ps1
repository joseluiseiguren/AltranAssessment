
$outputDir = "C:\Repos\BackEnd.Client\RestClient\";

rm -Recurse -Force $outputDir;

.\autorest -CodeGenerator CSharp -Modeler Swagger -Input http://localhost:21809/swagger/docs/v1 -Namespace BackEnd.Client -OutputDirectory $outputDir -AddCredentials False -SkipValidation -ClientName BackEndClient -debug
