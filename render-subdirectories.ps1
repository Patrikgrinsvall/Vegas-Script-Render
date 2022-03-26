# Note! This is completley untested and only works theoreticly. Written on linux so not even started.

$vegasBinary="c:\Program Files\vegas\vegas170.exe"

$projects = Get-ChildItem  *.veg -Depth 1
foreach($project in $projects)
{
  Invoke $vegasBinary $project -SCRIPT:"%~dp0\ScriptRender\EntryPoint.cs"
}
