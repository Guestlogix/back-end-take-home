This is a spring boot project and you need maven and java 1.8 to build it.
I have pushed the artifact, as well so you can just run it as follows.

Go to the folder which contains the jar file (output folder) and enter following command on terminal:
nohup java -jar demo-0.0.1-SNAPSHOT.jar &

you can see the log file on nohoup.out.

When the service started you can access the service with this Url:
http://localhost:8000/findRoutes?origin=JFK&destination=YYR

If you run this jar file on another machine please use that machine's IP instead of localhost.

This application listens on port 8000 which you can modify it on application.properties file in this path: /demo/src/main/resources then you need to build a new jar file.

If you modified the port or you run the application on another machine please don't forget to change the BASE_URL on DemoApplicationTests class, as well.

before running the test you need to run the service.


Please let me know if you have any questions.