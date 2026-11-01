# Summary:
#     Local development start script for the backend API.
#
# Description:
#     Cleans the obj folder, builds the project with `dotnet build`,
#     then runs the compiled Debug DLL directly via
#     `dotnet ./bin/Debug/net10.0/backend.dll`.
#     This avoids the extra memory overhead of `dotnet run`, which
#     keeps a build host process alive alongside the app.
#
# Usage:
#     sh run.sh
#

set -eu

# Summary:
#     Removes the obj folder so the next build starts clean.
#
rm -rf ./obj

dotnet build --nologo
echo "🚀 Starting..."

# Summary:
#     Runs the compiled backend DLL directly.
#     Replaces this shell process with the app to keep memory usage low.
#
exec dotnet ./bin/Debug/net10.0/backend.dll
