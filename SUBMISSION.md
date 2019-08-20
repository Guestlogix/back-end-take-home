Its a Spring Boot - JPA project.
LIquibase is used for schema generation, when you run this application schema will be automatically created.

You need to run the following betlow mentioned command to load the data in the tables: 

LOAD DATA LOCAL INFILE "path to file/airports.csv"
INTO TABLE guestlogix.airports
COLUMNS TERMINATED BY ','
LINES TERMINATED BY '\n'
IGNORE 1 LINES(name,city,country,iata3,latitute,longitude);


LOAD DATA LOCAL INFILE "path to file/airlines.csv"
INTO TABLE guestlogix.airlines
COLUMNS TERMINATED BY ','
LINES TERMINATED BY '\n'
IGNORE 1 LINES(name,2digit_code,3digit_code,country);


LOAD DATA LOCAL INFILE "path to file/routes.csv"
INTO TABLE guestlogix.routes
COLUMNS TERMINATED BY ','
LINES TERMINATED BY '\n'
IGNORE 1 LINES(airline_id,origin,destination);


Logs are created and pushed to a log folder, if your system need extra permission to make logback up and running, run the following commands:
sudo -i
mkdir /log
cd /log/
mkdir shortestRoute
chmod 777 /log/
chmod 777 shortestRoute/


Project is running at port 5051
Sample request : localhost:5051/shortest-route?origin=YYZ&destination=YVR