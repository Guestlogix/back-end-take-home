test:
	docker-compose up -d --build database app_test
	docker-compose run app_test sh /flight_router/test_entrypoint.sh
	docker-compose down
run:
	docker-compose up -d database app
	sh wait_seed.sh
stop:
	docker-compose down
