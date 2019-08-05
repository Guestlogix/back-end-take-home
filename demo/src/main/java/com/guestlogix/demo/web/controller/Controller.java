package com.guestlogix.demo.web.controller;

import com.guestlogix.demo.utilities.ResponseUtil;
import com.guestlogix.demo.web.ResponseBean;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


@RestController
public class Controller {

    @RequestMapping(value = "/findRoutes", method = RequestMethod.GET)
    @ResponseBody
    public ResponseEntity<ResponseBean> findShortestRoute(@RequestParam String origin,
                                                          @RequestParam String destination) {

        return ResponseUtil.prepareResponse(origin, destination);

    }

}
