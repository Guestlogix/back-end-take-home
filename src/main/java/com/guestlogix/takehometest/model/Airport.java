package com.guestlogix.takehometest.model;

public class Airport {

	private String name;
	private String city;
	private String country;
	private String iata_3;
	private String latitute;
	private String longitude;

	private Airport(String name, String city, String country, String iata_3, String latitute, String longitude) {
		super();
		this.name = name;
		this.city = city;
		this.country = country;
		this.iata_3 = iata_3;
		this.latitute = latitute;
		this.longitude = longitude;
	}

	public String getName() {
		return name;
	}

	public String getCity() {
		return city;
	}

	public String getCountry() {
		return country;
	}

	public String getIata_3() {
		return iata_3;
	}

	public String getLatitute() {
		return latitute;
	}

	public String getLongitude() {
		return longitude;
	}

	public static Airport create(String name, String city, String country, String iata_3, String latitute, String longitude) {
		return new Airport(name, city, country, iata_3, latitute, longitude);
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((city == null) ? 0 : city.hashCode());
		result = prime * result + ((country == null) ? 0 : country.hashCode());
		result = prime * result + ((iata_3 == null) ? 0 : iata_3.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Airport other = (Airport) obj;
		if (city == null) {
			if (other.city != null)
				return false;
		} else if (!city.equals(other.city))
			return false;
		if (country == null) {
			if (other.country != null)
				return false;
		} else if (!country.equals(other.country))
			return false;
		if (iata_3 == null) {
			if (other.iata_3 != null)
				return false;
		} else if (!iata_3.equals(other.iata_3))
			return false;
		return true;
	}

}
