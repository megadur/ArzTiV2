@echo off
SET ADD_PROP=

SET ADD_PROP=%ADD_PROP%targetFramework=net48,
SET ADD_PROP=%ADD_PROP%packageName=ArzTiClient,
SET ADD_PROP=%ADD_PROP%operationIsAsync=true,
SET ADD_PROP=%ADD_PROP%library=httpclient,

SET INFILE="./swagger.json"
SET OUTFILE="../Client/ArzTiCsharpNet"

rmdir %OUTFILE% /Q /S

ECHO java -jar openapi-generator-cli.jar generate -i %INFILE% -g csharp -o %OUTFILE% --additional-properties=%ADD_PROP% 
java -jar openapi-generator-cli.jar generate -i %INFILE% -g csharp -o %OUTFILE% --additional-properties=%ADD_PROP% 

::pause