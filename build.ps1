param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration="Release",

    [switch]$TestCoverage
)

dotnet build -c $Configuration "$PSScriptRoot/wimm.Confoundry.sln"

Get-ChildItem -Recurse -Include *UnitTests*proj | ForEach-Object {
    $project = $_.FullName
    $testCoverageArg = if ($TestCoverage) { "--logger:trx" } else { "" }
    dotnet test $testCoverageArg --no-build --no-restore -c $Configuration "$project"
}