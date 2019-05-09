### Some notes

The airline AC (which apparently stands for Air Canada) appears in routes.csv, but does not appear in airlines.csv. While this may be a typo, I made no assumptions and decided against changing all the routes with AC to CA.

Since this exercise only asks for a shortest route endpoint, I did not include any endpoints to retrieve airline/airport information, even if I did model them in the code and there is data for them.

Usually I would put all the data in a relational database (such as Microsoft SQL Server). For the purpose of this exercise, I think that may be more trouble than it's worth, and hence I simply load all the data into memory when the program starts.

The project includes a simple frontend for convenience.