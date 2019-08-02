from bfs import bfs;

def getFlightOptionsForRoute( route, connections_dic, airlines_info_dic, airports_info_dic):
	
	route_options = [];
	
	for  fr,to in zip(route[:-1], route[1:]):
		connection = {};
		connection["org"] = {};
		connection["org"]["code"] = fr;
		connection["org"]["city"] = airports_info_dic[fr]["City"];
		connection["org"]["airport"] = airports_info_dic[fr]["Name"];
		
		connection["dst"] = {};
		connection["dst"]["code"] = to;
		connection["dst"]["city"] = airports_info_dic[to]["City"];
		connection["dst"]["airport"] = airports_info_dic[to]["Name"];
		connection["flightOptions"] = [];
		for flight in connections_dic[fr][to]:
			connection["flightOptions"].append({
				"code":flight,
				"name":airlines_info_dic[flight]["Name"]
			});
			
		route_options.append(connection);
		
	return route_options;
		
		

def findRoute( org, dst , flight_data ):
	response = {};
		
	airlines_info_dic, airports_info_dic, connections_dic = flight_data;
	
	
	if org == dst :
		response["type"] = "Error";
		response["message"] = "Origin and Destination must be different:" + org; 
		return response;



	if not airports_info_dic.has_key(org):
		response["type"] = "Error";
		response["message"] = "Invalid Origin:" + org; 
		return response;

	if not airports_info_dic.has_key(dst):
		response["type"] = "Error";
		response["message"] = "Invalid Destination:" + dst; 
		return response;
	
	# use bfs to determine the mimimum connection route;		
	route = bfs(  org, dst, connections_dic);
	
	
	if len(route) == 0:
		response["type"] = "Failure";
		response["message"] = "No Route Exists"; 
	else:
		response["type"] = "Success";
		response["message"] = "Route Found"; 
		response["route"] = route;
		
		response["detailedRoute"] = getFlightOptionsForRoute( route, connections_dic, airlines_info_dic, airports_info_dic);
		
		
		
		
	return response;
