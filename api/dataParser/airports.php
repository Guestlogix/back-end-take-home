<?php
	/**
	* Airports Parser
	*/
	class Airports
	{
		private $data;
		private $idsPerCountry;

		function __construct()
		{
			$i = 0;
			if (($handle = fopen("../data/airports.csv", "r")) !== false) {
			  	while (($line = fgetcsv($handle, 1000, ",")) !== false) {
				    if($i > 0) {	
				    	if(empty($line) || !is_array($line)) continue;						    	
				    	$airportID = $line[3];
				    	if($airportID == "\N") continue;
				    	$this->data[$airportID] = new Airport($line);
				    	$country = $line[2];
				    	if(!isset($this->idsPerCountry[$country]))
				    		$this->idsPerCountry[$country] = array();
				    	array_push($this->idsPerCountry[$country], $airportID);
				    }
				    $i++;
				}
				fclose($handle);
			}
		}

		public function getData() {return $this->data;}

		//Haversine formula implementation
		public function getDistance($origin, $destination) 
		{
			$radiusOfEarth = 6371000;// Earth's radius in meters.
			if(!isset($this->data[$origin]) || !isset($this->data[$destination]))
				return -1;
			$originObj = $this->data[$origin];
			$destinationObj = $this->data[$destination];

	        $diffLatitude = $destinationObj->getLatitude() - $originObj->getLatitude();
	        $diffLongitude = $destinationObj->getLongitude() - $originObj->getLongitude();
	        $a = sin($diffLatitude / 2) * sin($diffLatitude / 2) +
	            cos($originObj->getLatitude()) * cos($destinationObj->getLatitude()) *
	            sin($diffLongitude / 2) * sin($diffLongitude / 2);
	        $c = 2 * asin(sqrt($a));
	        $distance = $radiusOfEarth * $c;
	        return $distance;
		}

		public function getAirportCountry($airportID)
		{
			return $this->data[$airportID]->getCountry();
		}

		public function getClosestAirportAtSameCountry($airportID, $country, $excludeIDs)
		{
			if(!isset($this->idsPerCountry[$country]) || count($this->idsPerCountry[$country]) == 1)
				return -1;
			if($excludeIDs && count($this->idsPerCountry[$country]) - count($excludeIDs) == 1)
				return -1;
			$otherAirportIDs = $this->idsPerCountry[$country];
			$i = 0;
			foreach ($otherAirportIDs as $otherAirportID) 
			{
				if($otherAirportID == $airportID) continue;
				if($excludeIDs && in_array($otherAirportID, $excludeIDs)) continue;
				if($i == 0) 
				{
					$closestAirportID = $otherAirportID;
					$closestAirportDistance = $this->getDistance($airportID, $closestAirportID);
				}
				else
				{
					$newAirportDistance = $this->getDistance($airportID, $otherAirportID);
					if($newAirportDistance < $closestAirportDistance)
					{
						$closestAirportID = $otherAirportID;
						$closestAirportDistance = $newAirportDistance;
					}
				}
				$i++;	
			}
			return $closestAirportID;
		}
	}

	/*
	* Holds Single Airport Info
	*/
	class Airport
	{
		private $name;
		private $city;
		private $country;
		private $iata3;
		private $latitude;
		private $longitude;

		function __construct($argsArr)
		{
			$this->name = $argsArr[0];			
			$this->city = $argsArr[1];
			$this->country = $argsArr[2];
			$this->iata3 = $argsArr[3];			
			$this->latitude = $argsArr[4];
			$this->longitude = $argsArr[5];
		}

		public function getName() {return $this->name;}
		public function getCity() {return $this->city;}		
		public function getCountry() {return $this->country;}
		public function getIata3() {return $this->iata3;}
		public function getLatitude() {return $this->latitude;}
		public function getLongitude() {return $this->longitude;}
	}
?>