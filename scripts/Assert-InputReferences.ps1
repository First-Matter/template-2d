$esc = "$([char]27)";
$TXT_CLEAR = "$esc[0m";
$TXT_RED = "$esc[91m";
$TXT_GREEN = "$esc[92m";


function Assert-InputReferences($Path) {
  $errors = 0
  Get-ChildItem -Path $Path -Filter *.cs -Recurse | # Find all .cs files
  Select-String -Pattern 'Input\.' | # Find all references to Input
  Select-Object -ExpandProperty Path | # Get the path of the file
  Where-Object { 
    $_ -notmatch 'InputBinding\.cs|PlayerInput\.cs' # Filter out PlayerInput.cs and InputBinding.cs
  } | 
  ForEach-Object {
    Write-Host $TXT_RED"$(Resolve-Path $_ -Relative) has an Input reference`nEnsure only PlayerInput.cs and InputBinding.cs reference Input"
    $errors++ # Increment the error count
  }
  if ($errors) { 
    exit 1 
  }
  else { 
    Write-Host $TXT_GREEN"All references to Input are valid!"$TXT_CLEAR
  } # Never let it happen again
}

Assert-InputReferences -Path $PSScriptRoot/../Assets/Scripts