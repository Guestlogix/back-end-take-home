package com.Guestlogix.shortestRoute.handler;

import javax.servlet.http.HttpServletRequest;

import org.springframework.web.filter.AbstractRequestLoggingFilter;

public class LoggingHandler extends AbstractRequestLoggingFilter {

	@Override
	protected void beforeRequest(HttpServletRequest request, String message) {}

	@Override
	protected void afterRequest(HttpServletRequest request, String message) {
		logger.debug(message);
	}

}
