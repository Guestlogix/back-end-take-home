### Some notes

The airline AC (which apparently stands for Air Canada) appears in routes.csv, but does not appear in airlines.csv. While this may be a typo, I made no assumptions and decided against changing all the routes with AC to CA.

Since this exercise only asks for a shortest route endpoint, I did not include any endpoints to retrieve airline/airport information, even if I did model them in the code and there is data for them.

Usually I would put all the data in a relational database (such as Microsoft SQL Server). For the purpose of this exercise, I think that may be more trouble than it's worth, and hence I simply load all the data into memory whenever necessary.

The project includes a simple frontend for convenience.

### How to run

I used Visual Studio 2015 Community, but I'm sure any version of VS will work. Simply double click Guestlogix.sln to open it in Visual Studio, and click the green triangle near the top that says Google Chrome to run the website. The frontend should automatically pop up.

### About the code

Because I used Visual Studio to create a default MVC project, a lot of the files I committed to Git are not related to the solution of the exercise. The important files to read through are as follows:

