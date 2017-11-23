<?php
	/*
	* Airlines Parser
	*/
	class Airlines
	{
		private $data;
		private $idsPerCountry;

		function __construct()
		{
			$i = 0;
			if (($handle = fopen("../data/airlines.csv", "r")) !== false) {
			  	while (($line = fgetcsv($handle, 100, ",")) !== false) {
				    if($i > 0) {
				    	$airlineID = $line[1];
				    	$country = $line[3];
				    	$this->data[$airlineID] = new Airline(["name"=>$line[0], "country"=>$line[3], "3_digit_code"=>$line[2]]);
				    	if(!isset($this->idsPerCountry[$country]))
				    		$this->idsPerCountry[$country] = array();
				    	array_push($this->idsPerCountry[$country], $airlineID);
				    }
				    $i++;
				}
				fclose($handle);
			}
		}

		public function getData() {return $this->data;}
	}

	/*
	* Holds Single Airline Info
	*/
	class Airline
	{
		private $name;
		private $country;
		private $threeDigitCode;

		function __construct($argsArr)
		{
			$this->name = $argsArr["name"];
			$this->country = $argsArr["country"];
			$this->threeDigitCode = $argsArr["3_digit_code"];
		}

		public function getName() {return $this->name;}
		public function getCountry() {return $this->country;}
		public function getThreeDigitCode() {return $this->threeDigitCode;}

	}
?>