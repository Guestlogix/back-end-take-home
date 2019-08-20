package com.Guestlogix.shortestRoute.util;

/**
 * 
 * @author nchopra
 *
 */
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Queue;
import java.util.Set;

import com.Guestlogix.shortestRoute.model.Node;

public class ShortestPathUtil {
	private static Map<String, ArrayList<String>> adjList = new HashMap<String, ArrayList<String>>();

	public static void addEdge(String or, String dest) {
		ArrayList<String> destList = adjList.get(or);
		if (destList != null) {
			destList.add(dest);
			adjList.put(or, destList);
		} else {
			ArrayList<String> destinations = new ArrayList<String>(0);
			destinations.add(dest);
			adjList.put(or, destinations);
		}
	}

	public static List<String> evaluateShortesPath(String origin, String destination) {
		Queue<Node> queue = new LinkedList<>();
		Set<String> visited = new HashSet<>();
		List<String> routes = new ArrayList<>();

		if (!adjList.containsKey(origin)) {
			return routes;
		} else {
			visited.add(origin);
			routes.add(origin);
			Node qNode = new Node(origin, 0, routes);
			queue.add(qNode);
			while (!queue.isEmpty()) {
				Node curNode = queue.remove();
				List<String> values = adjList.get(curNode.getCode());

				for (String v : values) {
					if (!visited.contains(v)) {
						List<String> finalRoute = new ArrayList<>(curNode.getNeighbours());
						finalRoute.add(v);
						Node dRoute = new Node(v, curNode.getDistance() + 1, finalRoute);
						queue.add(dRoute);
						if (dRoute.getCode().equalsIgnoreCase(destination)) {
							return dRoute.getNeighbours();
						}
						visited.add(v);
					}
				}

			}
		}
		return routes;
	}
}
