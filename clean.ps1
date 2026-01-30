# Clean Folders Script
# This script removes temporary and build-related folders from the project directory.

# get the current script directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir


# Define folders to clean + add base location
$foldersToClean = @(
  $scriptDir + "\docs", 
  $scriptDir + "\runtime\csharp\AgentSchema", 
  $scriptDir + "\runtime\csharp\AgentSchema.Tests", 
  $scriptDir + "\runtime\python\agentschema\src\agentschema\core",
  $scriptDir + "\runtime\python\agentschema\tests\core",
  $scriptDir + "\runtime\definition"
)

# print out the folders to be cleaned
Write-Host "Cleaning the following folders:"
foreach ($folder in $foldersToClean) {
  # print out each folder with a line break
  Write-Host $folder
}