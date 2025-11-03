@echo off
SET ADD_PROP=

SET ADD_PROP=%ADD_PROP%targetFramework=netstandard2.1,
SET ADD_PROP=%ADD_PROP%packageName=ArzTiClient,
SET ADD_PROP=%ADD_PROP%operationIsAsync=true,

SET INFILE="./swagger.json"
SET OUTFILE="../Client/ArzTiCsharpNetcore"

::del %OUTFILE% /Q

start openapi-generator-cli generate -i %INFILE% -g csharp-netcore -o %OUTFILE% --additional-properties=%ADD_PROP% 

pause