package com.guestlogix.demo.utilities;

import com.guestlogix.demo.model.Airport;
import com.guestlogix.demo.web.ResponseBean;
import org.springframework.http.ResponseEntity;

import java.util.LinkedList;
import java.util.Objects;
import java.util.Optional;

public class ResponseUtil {

    public static ResponseEntity<ResponseBean> prepareResponse(String origin, String destination) {

        Optional<Airport> originAirport = DataUtil.airports.parallelStream()
                .filter(Objects::nonNull)
                .filter(a -> a.getIata().equals(origin.toUpperCase()))
                .findFirst();

        Optional<Airport> destAirport = DataUtil.airports.parallelStream()
                .filter(Objects::nonNull)
                .filter(a -> a.getIata().equals(destination.toUpperCase()))
                .findFirst();


        if (!originAirport.isPresent()) {

            ResponseBean response = new ResponseBean("Invalid Origin");
            return ResponseEntity.ok(response);
        }


        if (!destAirport.isPresent()) {

            ResponseBean response = new ResponseBean("Invalid Destination");
            return ResponseEntity.ok(response);

        }

        DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(DataUtil.graph);
        dijkstra.execute(originAirport.get());
        LinkedList<Airport> path = dijkstra.getPath(destAirport.get());

        if (path != null && path.size() > 0) {

            ResponseBean response = ResponseBean.factory(path);
            return ResponseEntity.ok(response);


        } else {
            ResponseBean response = new ResponseBean("No Route");
            return ResponseEntity.ok(response);

        }
    }
}
