#!/bin/bash
cd service && dotnet restore && dotnet build &&
cd ../tests && dotnet restore && dotnet build && dotnet test --logger:"console;verbosity=detailed"