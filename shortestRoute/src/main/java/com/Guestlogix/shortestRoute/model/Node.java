package com.Guestlogix.shortestRoute.model;

/**
 * 
 * @author nchopra
 *
 */
import java.util.List;

public class Node {
	private String code;
	private int distance;
	private List<String> neighbours;

	public Node() {
	}

	public Node(String code, int distance, List<String> neighbours) {
		this.code = code;
		this.distance = distance;
		this.neighbours = neighbours;
	}

	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	public int getDistance() {
		return distance;
	}

	public void setDistance(int distance) {
		this.distance = distance;
	}

	public List<String> getNeighbours() {
		return neighbours;
	}

	public void setNeighbours(List<String> neighbours) {
		this.neighbours = neighbours;
	}

}
