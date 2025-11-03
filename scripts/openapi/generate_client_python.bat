@echo off
SET ADD_PROP=

SET ADD_PROP=%ADD_PROP%packageName=arzti_client,

SET INFILE="./swagger.json"
SET OUTFILE="../Client/ArzTiPython"

::del %OUTFILE% /Q

start openapi-generator-cli generate -i %INFILE% -g python -o %OUTFILE% --additional-properties=%ADD_PROP% 

pause