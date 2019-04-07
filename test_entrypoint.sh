#!/bin/bash
echo "Looking up for database"
while ! curl http://database:7474
do
		sleep 5;
done
echo "Running tests"
python tests.py
