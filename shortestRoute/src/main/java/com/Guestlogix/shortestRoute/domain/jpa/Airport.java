package com.Guestlogix.shortestRoute.domain.jpa;

import static javax.persistence.GenerationType.IDENTITY;

import java.io.Serializable;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.validation.constraints.NotBlank;

import org.springframework.cache.annotation.Cacheable;

/**
 * 
 * @author nchopra
 *
 */
@Entity
@Table(name = "airports")
@Cacheable(value = "airports")
public class Airport implements Serializable {

	private static final long serialVersionUID = -1763574516474353414L;
	@Id
	@GeneratedValue(strategy = IDENTITY)
	@Column(name = "id", unique = true, nullable = false)
	private Long id;

	@NotBlank(message = "name is required")
	@Column(name = "name", nullable = false)
	private String name;

	@NotBlank(message = "city is required")
	@Column(name = "city", nullable = false)
	private String city;

	@NotBlank(message = "country is required")
	@Column(name = "country", nullable = false)
	private String country;

	@NotBlank(message = "iata3 is required")
	@Column(name = "iata3", nullable = false)
	private String iata3;

	@NotBlank(message = "latitute is required")
	@Column(name = "latitute", nullable = false)
	private String latitute;

	@NotBlank(message = "longitude is required")
	@Column(name = "longitude", nullable = false)
	private String longitude;

	@Temporal(TemporalType.DATE)
	@Column(name = "created_on")
	private Date createdOn;

	@Temporal(TemporalType.DATE)
	@Column(name = "modified_on")
	private Date modifiedOn;

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getCity() {
		return city;
	}

	public void setCity(String city) {
		this.city = city;
	}

	public String getCountry() {
		return country;
	}

	public void setCountry(String country) {
		this.country = country;
	}

	public String getIata3() {
		return iata3;
	}

	public void setIata3(String iata3) {
		this.iata3 = iata3;
	}

	public String getLatitute() {
		return latitute;
	}

	public void setLatitute(String latitute) {
		this.latitute = latitute;
	}

	public String getLongitude() {
		return longitude;
	}

	public void setLongitude(String longitude) {
		this.longitude = longitude;
	}

	public Date getCreatedOn() {
		return createdOn;
	}

	public void setCreatedOn(Date createdOn) {
		this.createdOn = createdOn;
	}

	public Date getModifiedOn() {
		return modifiedOn;
	}

	public void setModifiedOn(Date modifiedOn) {
		this.modifiedOn = modifiedOn;
	}

}
