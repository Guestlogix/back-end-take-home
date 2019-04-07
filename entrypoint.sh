#!/bin/bash
while ! curl http://database:7474
do
		sleep 5;
done
echo "seeding airline"
python manage.py seed_airlines --fname=data/airlines.csv
echo "seeding airports"
python manage.py seed_airports --fname=data/airports.csv
echo "seeding routes"
python manage.py seed_routes --fname=data/routes.csv
echo "seeding is over"
python manage.py runserver
