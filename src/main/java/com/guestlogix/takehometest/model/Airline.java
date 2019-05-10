package com.guestlogix.takehometest.model;

public class Airline {

	private String name;
	private String digitalCode2;
	private String digitalCode3;
	private String country;

	private Airline(String name, String digitalCode2, String digitalCode3, String country) {
		super();
		this.name = name;
		this.digitalCode2 = digitalCode2;
		this.digitalCode3 = digitalCode3;
		this.country = country;
	}

	public String getName() {
		return name;
	}
	public String getDigitalCode2() {
		return digitalCode2;
	}
	public String getDigitalCode3() {
		return digitalCode3;
	}
	public String getCountry() {
		return country;
	}
	
	public static Airline create(String name, String digitalCode2, String digitalCode3, String country) {
		return new Airline(name, digitalCode2, digitalCode3, country);
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((country == null) ? 0 : country.hashCode());
		result = prime * result + ((digitalCode2 == null) ? 0 : digitalCode2.hashCode());
		result = prime * result + ((digitalCode3 == null) ? 0 : digitalCode3.hashCode());
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
		Airline other = (Airline) obj;
		if (country == null) {
			if (other.country != null)
				return false;
		} else if (!country.equals(other.country))
			return false;
		if (digitalCode2 == null) {
			if (other.digitalCode2 != null)
				return false;
		} else if (!digitalCode2.equals(other.digitalCode2))
			return false;
		if (digitalCode3 == null) {
			if (other.digitalCode3 != null)
				return false;
		} else if (!digitalCode3.equals(other.digitalCode3))
			return false;
		return true;
	}

}
