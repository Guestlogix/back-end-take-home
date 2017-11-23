<?php
	foreach (glob("dataParser/*.php") as $filename)
	{
		include $filename;
	}
	include "dijkstraAlgo/Dijkstra.php";

	$origin = $_GET['origin'];
	$destination = $_GET['destination'];
	
	$airlines = new Airlines();	
	$airports = new Airports();

	$g = new Graph();
	$i = 0;
	if (($handle = fopen("../data/routes.csv", "r")) !== false) {
	  	while (($route = fgetcsv($handle, 100, ",")) !== false) {
		    if($i > 0) {
		    	$routeOrigin = $route[1];
		    	$routeDestination = $route[2];		    	
		    	$weight = $airports->getDistance($routeOrigin, $routeDestination);
		    	if($weight == -1) continue;
		    	$g->addedge($routeOrigin, $routeDestination, $weight);
		    }
		    $i++;
		}
		fclose($handle);
	}

	$path = $g->getpath($origin, $destination);
	if(count($path) == 0){
		$country = $airports->getAirportCountry($origin);
		echo "<br/> No path found! We are searching for paths from other airports at $country. </br>";
		$excludeIDs = array();
		while (count($path) == 0) 
		{
			$otherAirport = $airports->getClosestAirportAtSameCountry($origin, $country, $excludeIDs);
			if($otherAirport == -1)
			{
				$noPathFound = true;
				echo "<br/> There is no path from $country to $destination airport <br/>";
				break;
			}
			$path = $g->getpath($otherAirport, $destination);
			array_push($excludeIDs, $otherAirport);
		}
		if(!$noPathFound)
		{
			echo "<br/> We found an alternative path for you! <br/>";
			echo implode(" -> ", $path);
		}
	}
	else{
		echo "<br/> Shortest Path from $origin to $destination: </br>";
		echo implode(" -> ", $path);
	}
?>