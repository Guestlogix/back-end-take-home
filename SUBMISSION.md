### Project information

The project was basically created using Java 8 , Spring Boot and Maven as project configuration.
To run the project you by command line, using maven, run the code below:

mvn package

and then, run the code below in the target folder(where the jar file was generated)

java -jar <jar_file>

to test the application, using maven, run the command below

mvn test

### Important points

I noticed some problems matching the airlines in routes file. The AC airline (Air China) did not appear in routes file, but the CA in routes file did not appear in airlines file. So, for no presumptions, i did not change the file and during the process of loading the routes file, the airlines i did not found in airlines file, i ignored them.


