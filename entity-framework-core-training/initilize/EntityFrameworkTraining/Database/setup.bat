@echo off
REM setting parameters
SET SERVER=(localdb)\mssqllocaldb

sqlcmd -S %SERVER% -E -i CreateMembershipData.sql
sqlcmd -S %SERVER% -E -i InsertShopData.sql

PAUSE
