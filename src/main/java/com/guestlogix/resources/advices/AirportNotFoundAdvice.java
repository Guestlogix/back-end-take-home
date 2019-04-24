package com.guestlogix.resources.advices;

import com.guestlogix.resources.exceptions.AirportNotFoundException;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.ResponseStatus;

/**
 * Whenever and AirportNotFoundException is thrown, Spring will use this class to render the correct answer for the user.
 *
 * @author Vanderson Assis
 * @since 4/24/2019
 */

@ControllerAdvice
public class AirportNotFoundAdvice {
    @ResponseBody
    @ExceptionHandler(AirportNotFoundException.class)
    @ResponseStatus(HttpStatus.NOT_FOUND)
    String airportNotFoundHandler(AirportNotFoundException ex) {
        return ex.getMessage();
    }
}
