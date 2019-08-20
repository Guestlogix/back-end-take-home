package com.Guestlogix.shortestRoute.domain.network.response;

import java.io.Serializable;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonInclude.Include;

/**
 * 
 * @author nchopra
 *
 */
@JsonInclude(Include.NON_NULL)
public class BaseResponse implements Serializable {

	private static final long serialVersionUID = -1898217599935891882L;
	private int status;
	private String error;
	private String errorCode;
	private Object errorDesc;

	public int getStatus() {
		return status;
	}

	public void setStatus(int status) {
		this.status = status;
	}

	public String getError() {
		return error;
	}

	public void setError(String error) {
		this.error = error;
	}

	public Object getErrorDesc() {
		return errorDesc;
	}

	public void setErrorDesc(Object errorDesc) {
		this.errorDesc = errorDesc;
	}

	public String getErrorCode() {
		return errorCode;
	}

	public void setErrorCode(String errorCode) {
		this.errorCode = errorCode;
	}

}
