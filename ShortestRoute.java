import java.io.BufferedReader;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.HashMap;
import java.util.Set;
import java.util.Queue;
import java.util.LinkedList;
import java.util.HashSet;

public class ShortestRoute {
	private static Boolean isDestinationFound = true;
	public static void main(String[] args)  throws java.io.IOException  {
		String origin = args[0].toUpperCase();
		String destination = args[1].toUpperCase();
		isDestinationFound = true;
		HashMap<String, List<String>> routesMap = readData("data/full/routes.csv", destination) ;
				
		if (!routesMap.containsKey(origin)){
			System.out.println("Invalid Origin");
			return;
			
		} else if (!isDestinationFound) {
			System.out.println("Invalid Destination");
			return;
			
		}
		List<String> routes = findPath(routesMap, origin, destination);
		if(routes.size() == 1){
			System.out.println("No Route");
			
		}else{
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i<routes.size(); i++){
				sb = sb.append(routes.get(i));
				sb.append("->");
			}
			
			sb.deleteCharAt(sb.length()-1);
			sb.deleteCharAt(sb.length()-1);
			System.out.print(sb);
			
		}
		
		
	}
	
	public static HashMap<String, List<String>> readData(String fileName, String destination) throws java.io.IOException {
		HashMap<String, List<String>> map = new HashMap<>();
		Path path = Paths.get(fileName);
		Set<String> availableDestinations = new HashSet<>();
		
		BufferedReader br = Files.newBufferedReader(path, StandardCharsets.US_ASCII);
		String line = br.readLine();
		while ((line = br.readLine()) != null) {
			String origin = line.split(",")[1];
			String dest = line.split(",")[2];
						
			map.putIfAbsent(origin, new ArrayList<>());
			map.get(origin).add(dest);
			availableDestinations.add(dest);
		}			
		
		if(!availableDestinations.contains(destination)){
			
			isDestinationFound = false;
			
		}
		
		return map;
	}
	
	public static class Distance {
		String code;
		int distance;
		List<String> codes;
		Distance(String c, int d, List<String> _codes) {
			code = c;
			distance = d;
			codes = _codes;
			
		}
		
	}
	public static List<String> findPath(HashMap<String, List<String>> routesMap, String origin, String destination) {
		Queue<Distance> queue = new LinkedList<>();//(yvr,1,[lax,yvr]),(jfk,1,)
		Set<String> visited = new HashSet<>();
		List<String> routes = new ArrayList<>();
		
		if(!routesMap.containsKey(origin)){
			return routes;
		}else{
			visited.add(origin);//c
			routes.add(origin);//c
			Distance queueNode = new Distance(origin, 0, routes);//(lax, 0, [lax])
			
			queue.add(queueNode);			
			int curLevel = 0;
			while(!queue.isEmpty()){
				Distance curNode = queue.remove();//(lax, 0, [lax]				
				List<String> values = routesMap.get(curNode.code);
				
				for (String val: values) {
					if(!visited.contains(val)){
						List<String> routes2 = new ArrayList<>(curNode.codes);//lax
						routes2.add(val);//[lax, yvr]
						Distance destinationRoute = new Distance(val, curNode.distance + 1,routes2);
						queue.add(destinationRoute);//(yvr,1,[lax,yvr])
						if(destinationRoute.code.equals(destination)) {
							return destinationRoute.codes;
						}
						visited.add(val);//lax,yvr
					}
				}			
				
			}
		}
		return routes;
	}
}
