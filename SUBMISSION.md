# GuestLogix Challenge by Vanderson

## Getting Started
- Please clone this repository to your pc.
- Open the project using IntelliJ IDEA.
- Run the main method BackEndTakeHomeApplication.main. 
- After running the main method, please wait around 40 seconds for the project to start, database initialization and all the initial persisting from the CSV files.
- When the application is up, you'll be able to access the generated database in the following URL http://localhost:8080/h2-console. In it you'll be able to see all the relations and data that was generated for this project and run SQL queries.
  The interface displayed should have Driver Class: org.h2.Driver, JDBC Url jdbc:h2:~/db_guestlogix (this is the location where the database will be created), User Name: sa and Password: empty. Then you click the Connect button and you'll good to go.
- To use the algorithms that find the routes, please access the following URL http://localhost:8080/route/calculate/{originAirportIataCode}/{destinationAirportIataCode}. An example of execution is ``http://localhost:8080/route/calculate/CMH/YEG``
  After accessing the mentioned URL passing the Airport Iata Codes, the system will calculate and return a String with the routes found or a message informing no route was found.
  
  Quick mention here, I wanted to create a simple page where there would be a combo box for the user to select the Aiport by name 
  without having to know it's IATA code, but the time ran short. =/  

## Technologies Used:
- Java8
- Spring Technologies
- H2 Database
- IntelliJ IDEA
- JUnit
- Maven

### More:
In case of any doubt, please e-mail me at assis.vanderson@gmail.com, I'll be more than pleased to talk to you.