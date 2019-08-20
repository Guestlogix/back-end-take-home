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

/**
 * 
 * @author nchopra
 *
 */
@Entity
@Table(name = "airlines")
public class Airline implements Serializable {

	private static final long serialVersionUID = -1763574516474353414L;
	@Id
	@GeneratedValue(strategy = IDENTITY)
	@Column(name = "id", unique = true, nullable = false)
	private Long id;

	@NotBlank(message = "name is required")
	@Column(name = "name", nullable = false)
	private String name;

	@NotBlank(message = "country is required")
	@Column(name = "country", nullable = false)
	private String country;

	@NotBlank(message = "2digit_code is required")
	@Column(name = "2digit_code", nullable = false)
	private String twodigitCode;

	@NotBlank(message = "3digit_code is required")
	@Column(name = "3digit_code", nullable = false)
	private String threedigitCode;

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

	public String getCountry() {
		return country;
	}

	public void setCountry(String country) {
		this.country = country;
	}

	public String getTwodigitCode() {
		return twodigitCode;
	}

	public void setTwodigitCode(String twodigitCode) {
		this.twodigitCode = twodigitCode;
	}

	public String getThreedigitCode() {
		return threedigitCode;
	}

	public void setThreedigitCode(String threedigitCode) {
		this.threedigitCode = threedigitCode;
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
