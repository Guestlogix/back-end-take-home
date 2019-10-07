# RUNNING & INSTALL INSTRUCTIONS

1. Install dotnet core 2.2 on local machine
2. Run `start.bat` (windows) or `start.sh` (linux or mac) file located at root directory

After above two steps you are able to find the path (first requisition might be slow because it loads all data in memory).

Endpoint format is `http://localhost:[PORT]/api/routes?origin=[ORIGIN]&destination=[DESTINATION]`

Example:

    `http://localhost:5000/api/routes?origin=YYZ&destination=YVR` 

