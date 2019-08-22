The following solution was implemented as a .net mvc web application.

I used the following tools to run the solution:
- Microsoft SQL server 2017 (management tools 2018)
- Visual studio code (make sure dontnet packages are downloaded and installed)
- Postman
- Notepad++

Intructions on how to run solution:
1) I created a SQL script called GuestlogixDatabase.sql. This script will create the database
   along with the tables and will populate them.

   Things to consider when running script:
   a) Make sure to change the file path of the Bulk Inserts to their corresponding csv file in
      your file system.
   b) In order for the script to populate the tables correctly, I had to modify the "airport.csv"
      by opening it in notepad++ and replacing ", " (yes, it is a comma with a whitespace after it)
      with "\". Bulk insert was having issues with commas inside double quotes.

2) Create a user that has access to the newly created "Guestlogix" database.

3) Once the database is created and tables are populated, open the solution (should be in folder 
   called "Guestlogix") in visual studio code and go to "appsettings.json". Change the connection string
   Data source to your corresponding SQL server and the UserID/Password to the user that was created
   in the previous step.

4) Try building it using the command "dontnet build" in the (visual studio code) terminal. If the command 
   doesn't run or error occurs, it will prompt you to install CLI tools as well as .net core 2.X.X..
   Visual studio code normally does this automatically if it doesn't recognize the command.

5) After build it successful. Run the solution with the command "dotnet run". I don't expect many errors
   to appear at this step but the one that could show up depends whether or not the app successfully
   logged to the newly "Guestlogix" database in SQL server.

6) If applications runs successfully, it will be listening to port 5000.


My solution currently has 4 requests available:
GET api/routes/all                               --gets all routes in database
GET api/airports/all                             --gets all airports in database
GET api/airlines/all                             --gets all airlines in database
GET api/routes/shortest?origin={}&destination{}  --gets shortest path from origin to destination

EX:     localhost:5000/api/routes/shortest?origin=YYZ&destination=BOG
RESULT: YYZ => BOG

Note: Algorithm used to find shortest path is a variation of Breadth First Search.


