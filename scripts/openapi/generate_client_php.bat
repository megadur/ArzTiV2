@echo off
SET ADD_PROP=

SET ADD_PROP=%ADD_PROP%developerOrganization=ArzSw,

SET INFILE="./swagger.json"
SET OUTFILE="../Client/ArzTiPhp"

::del %OUTFILE% /Q

start openapi-generator-cli generate -i %INFILE% -g php -o %OUTFILE% --additional-properties=%ADD_PROP% 

pause