properties { 
  $base_dir  = $psake.build_script_dir
  $sln_file = "$base_dir\Boru.sln" 
  $version = "1.0.0.0"
  $configuration = "Debug"

  $src_dir = "$base_dir\src"
  $tools_dir = "$base_dir\tools"
  $tests_dir = "$base_dir\tests"

  $nuget = "$tools_dir\NuGet.exe"
  $xunit = "$tools_dir\xunit.runners.1.9.2\xunit.console.clr4.exe"
}


Task default -Depends Test

task Test -depends Compile {
    Write-Host "xUnit location: $xunit"
    
    Get-ChildItem $tests_dir -Recurse -Include *Tests.csproj | % {
        $project = $_.BaseName
        Exec { &"$xunit" "$tests_dir\$project\bin\$configuration\$project.dll" }
    }
}

Task Compile -Depends Clean, Packages {
    Exec { msbuild "$sln_file" /p:Configuration=$configuration }
}

task Packages {
    Write-Host "nuget location: $nuget"
    $nugetConfigs = Get-ChildItem $base_dir -Recurse | ?{$_.name -match "packages\.config"} | select
    foreach ($nugetConfig in $nugetConfigs) {
      Write-Host "restoring packages from $($nugetConfig.FullName)"
      Exec { &"$nuget" install $($nugetConfig.FullName) /OutputDirectory packages }
    }
}

Task Clean {
    Exec { msbuild "$sln_file" /t:Clean /p:Configuration=$configuration /v:quiet }
}