import csv

def buildConnectionsMap(base_dir):
	
	connections_dic ={};

	with open( base_dir + "routes.csv", mode='r') as csv_file:
		csv_reader = csv.DictReader(csv_file)
		for row in csv_reader:
			try:
				if not connections_dic.has_key(row["Origin"]):
					connections_dic[row["Origin"]] = {};
				if not connections_dic[row["Origin"]].has_key(row["Destination"]):
					connections_dic[row["Origin"]][row["Destination"]] = [];			
				connections_dic[row["Origin"]][row["Destination"]].append(row["Airline Id"]);
			except:
				print "Invalid Row: @" +base_dir + "routes.csv :" + row;
	

	return  connections_dic ;


def loadAirlinesData(base_dir):
	
	airlines_info_dic ={};
	
	with open( base_dir + "airlines.csv", mode='r') as csv_file:
		csv_reader = csv.DictReader(csv_file)
		for row in csv_reader:
			try:		
				airlines_info_dic[row["2 Digit Code"]] = row;
			except:
				print "Invalid Row: @" +base_dir + "airlines.csv :" + row;
				continue;
	
	return airlines_info_dic;
			
def loadAirportsData(base_dir):

	airports_info_dic ={};
	
	with open( base_dir + "airports.csv", mode='r') as csv_file:
		csv_reader = csv.DictReader(csv_file)
		for row in csv_reader:
			try:	
				airports_info_dic[ row["IATA 3"]] = row;
			except:
				print "Invalid Row: @" +base_dir + "airports.csv :" + row;


	return airports_info_dic;
	
def load(base_dir):
	return ( loadAirlinesData(base_dir), loadAirportsData(base_dir), buildConnectionsMap(base_dir))
	