create database Guestlogix;

-- run the script below after creating database
use Guestlogix;

create table Route(
	AirlineId nvarchar(2) not null,
	Origin nvarchar(3) not null,
	Destination nvarchar(3) not null
);

create table Airline(
	Name nvarchar(200) not null,
	DigitCode2 nvarchar(2) not null,
	DigitCode3 nvarchar(3) not null,
	Country nvarchar(50) not null
);

create table Airport(
	Name nvarchar(200) not null,
	City nvarchar(50) not null,
	Country nvarchar(50) not null,
	IATA3 nvarchar(3) not null,
	Latitude decimal(11,8),
	Longitude decimal(11,8)
);

BULK INSERT Airline FROM 'D:\Codespace\airlines.csv' WITH (FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', ERRORFILE = 'D:\Codespace\airlineError.txt', TABLOCK);
BULK INSERT Airport FROM 'D:\Codespace\airports.csv' WITH (FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', ERRORFILE = 'D:\Codespace\airportError.txt', TABLOCK);
BULK INSERT Route FROM 'D:\Codespace\routes.csv' WITH (FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', ERRORFILE = 'D:\Codespace\routeError.txt', TABLOCK);