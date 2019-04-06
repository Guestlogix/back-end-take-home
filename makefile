test:
	docker run -d \
		--publish=7474:7474 --publish=7687:7687 \
		--name="back-end-take-home-neo4j" \
		--env=NEO4J_AUTH=neo4j/testpwd \
		neo4j

	while ! curl http://localhost:7474; do\
		sleep 5; \
	done; \

	python tests.py
	docker stop "back-end-take-home-neo4j"
	docker rm "back-end-take-home-neo4j"
