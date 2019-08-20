package com.Guestlogix.shortestRoute.exceptions;

/**
 * 
 * @author nchopra
 *
 */
public enum ApiExCode implements ExceptionCodes {

	ORIGIN_NOT_FOUND("GL_OR404", "Invalid Origin"), DESTINATION_NOT_FOUND("GL_DT404", "Invalid Destination"), NO_ROUTE(
			"GL_NR404", "No Route"), INTERNAL_SERVER_ERROR("GL_500", "Internal Server Error"),;

	private String code;
	private String message;

	ApiExCode(String code, String message) {
		this.code = code;
		this.message = message;
	}

	@Override
	public String getCode() {
		return code;
	}

	@Override
	public String getMessage() {
		return message;
	}
}
