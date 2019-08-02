
def bfs( org, dst, graph_dic ):

	visited ={};
	prev={};
	queue = [];
	
	queue.append( org);
	visited[org] = True;
	prev[org] = None;
	
	bRouteFound = False;	
	
	while queue:
		
		currentNode = queue.pop(0);

		if currentNode == dst:
			bRouteFound = True;
			break;
		
		if not graph_dic.has_key(currentNode):
			continue;
			
		for neighbour in graph_dic[currentNode].keys():
			if not visited.has_key(neighbour):
				queue.append(neighbour);
				visited[neighbour]=True;
				prev[neighbour] = currentNode;

	route=[];
	
	if bRouteFound:
	
		node = dst;
		while prev[node]:
			route = [node] + route;
			node = prev[node];
		
		route = [org] + route;
			
	return route
		
