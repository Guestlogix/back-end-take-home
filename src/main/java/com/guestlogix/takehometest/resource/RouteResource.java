package com.guestlogix.takehometest.resource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.guestlogix.takehometest.exception.ParametersNotFoundException;
import com.guestlogix.takehometest.service.PathDto;
import com.guestlogix.takehometest.service.RouteService;

@RequestMapping("/route")
@RestController
public class RouteResource {

	@Autowired
	private RouteService routeService;

	@GetMapping
	public PathDto retrieveBestRoute(@RequestParam(name = "origin") String origin, @RequestParam(name = "dest") String dest) throws ParametersNotFoundException {
		return routeService.retrieveBestRoute(origin, dest);
	}

	@ExceptionHandler(ParametersNotFoundException.class)
	public ResponseEntity<ErrorDto> handleException(ParametersNotFoundException ex) {
		return new ResponseEntity<ErrorDto>(ErrorDto.create(ex.getMsg()), HttpStatus.INTERNAL_SERVER_ERROR);
	}

	static class ErrorDto {
		String msg;
		private ErrorDto(String msg) {
			this.msg = msg;
		}
		static ErrorDto create(String msg) {
			return new ErrorDto(msg);
		}
		public String getMsg() {
			return msg;
		}
	}
}
