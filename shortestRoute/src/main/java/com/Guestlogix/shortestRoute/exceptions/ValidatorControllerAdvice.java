package com.Guestlogix.shortestRoute.exceptions;
/**
 * 
 * @author nchopra
 *
 */
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.ResponseEntity;
import org.springframework.util.CollectionUtils;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestControllerAdvice;
import org.springframework.web.context.request.WebRequest;

import com.Guestlogix.shortestRoute.domain.network.response.BaseResponse;


@RestControllerAdvice(basePackages = {"com.Guestlogix.shortestRoute"} )
@ControllerAdvice(basePackages = {"com.Guestlogix.shortestRoute"} )
public class ValidatorControllerAdvice {
	
	private final Logger logger = LoggerFactory.getLogger(this.getClass());
	
    @ResponseBody
    @ExceptionHandler(ApiException.class)
    public ResponseEntity<?> authExceptionHandler(ApiException ex, WebRequest request) {
        BaseResponse response = new BaseResponse();
        Map<String, String> errors = ex.getErrors();
        if(!CollectionUtils.isEmpty(errors)) {
        	for (Map.Entry<String,String> entry : errors.entrySet())  {
        		response.setError(entry.getKey());
        		response.setErrorCode(entry.getValue());
        		response.setErrorDesc(ex);
        	}
        }
        logger.error(response.toString());
        return ResponseEntity.ok(response);
    }
} 