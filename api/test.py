from os import path;
import flightsDataLoader;
import routeFinder;

def test():
	data_dir = ".." + path.sep + "data" + path.sep + "test" + path.sep ;
	flights_data = flightsDataLoader.load(data_dir);
	print routeFinder.findRoute("YYZ","JFK",flights_data);
	print routeFinder.findRoute("YYZ","YVR",flights_data);
	print routeFinder.findRoute("YYZ","ORD",flights_data);
	print routeFinder.findRoute("XXX","JFK",flights_data);
	print routeFinder.findRoute("YYZ","XXX",flights_data);
	print routeFinder.findRoute("JFK","YYR",flights_data);
		

if __name__ == '__main__':
	test();
	
	