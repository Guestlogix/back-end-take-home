package com.guestlogix.takehometest.exception;

public class ParametersNotFoundException extends Exception {

	private static final long serialVersionUID = -6908069440168378826L;

	private String msg;

	public ParametersNotFoundException(String msg) {
		this.msg = msg;
	}

	public String getMsg() {
		return msg;
	}

}
