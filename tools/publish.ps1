Set-Location .\MoreSplashScreen
dotnet publish -c Release -p:CreateCipx=true -p:CipxPackageOutputDirectory=../out
Set-Location ..