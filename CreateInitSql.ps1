dotnet ef migrations add InitialCreate
dotnet ef migrations script -o init.sql

# Define source and destination paths
$sourceFile = "init.sql"
$destDir = "..\db"
$destFile = Join-Path $destDir "init.sql"

$migrationsFolder = "./Migrations"

# Delete all contents in the Migrations folder
if (Test-Path $migrationsFolder) {
    Remove-Item -Path "$migrationsFolder\*" -Recurse -Force
    Write-Host "Deleted all contents in the $migrationsFolder folder."
} else {
    Write-Host "The $migrationsFolder folder does not exist."
}

# Create destination directory if it does not exist
if (-not (Test-Path $destDir)) {
    New-Item -ItemType Directory -Path $destDir -Force
}

# Move the init.sql file and overwrite if it exists
if (Test-Path $sourceFile) {
    Move-Item -Path $sourceFile -Destination $destFile -Force
    Write-Host "Moved $sourceFile to $destFile and overwrote existing file."
} else {
    Write-Host "Source file $sourceFile does not exist."
}
