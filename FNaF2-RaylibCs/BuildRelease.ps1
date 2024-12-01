dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:OutputType=WinExe
Write-Output "Done! Starting release build..."
Set-Location (Join-Path (Split-Path $pwd) "FNaF2-RaylibCs\bin\Release\net8.0\win-x64")
Remove-Item -Recurse -Force (Join-Path (Split-Path $pwd) "FNaF2-RaylibCs\bin\Release\net8.0\win-x64\publish")
Start-Process -FilePath .\FNaF2-RaylibCs.exe
Write-Output "Done! Enjoy Testing!"
