package com.guestlogix.demo.web;

import com.guestlogix.demo.model.Airline;
import com.guestlogix.demo.model.Airport;
import com.guestlogix.demo.model.PathDetail;
import com.guestlogix.demo.model.Route;
import com.guestlogix.demo.utilities.DataUtil;

import java.util.*;

public class ResponseBean {

    private String message;
    private String shortestPath;
    private List<PathDetail> detail;


    public ResponseBean() {
    }

    public ResponseBean(String message) {
        this.message = message;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getShortestPath() {
        return shortestPath;
    }

    public void setShortestPath(String shortestPath) {
        this.shortestPath = shortestPath;
    }

    public List<PathDetail> getDetail() {
        return detail;
    }

    public void setDetail(List<PathDetail> detail) {
        this.detail = detail;
    }

    public static ResponseBean factory(LinkedList<Airport> path) {
        ResponseBean bean = new ResponseBean();
        bean.message = "Shortest Path Found: ";
        bean.shortestPath = getShortestPath(path);
        bean.detail = getPathDetail(path);
        return bean;
    }

    private static List<PathDetail> getPathDetail(LinkedList<Airport> pathList) {

        List<PathDetail> details = new ArrayList<>();


        for (int i = 0; i < pathList.size() - 1; i++) {

            PathDetail pathDetail = new PathDetail();

            Airport org = pathList.get(i);
            Airport dest = pathList.get(i + 1);

            pathDetail.setOriginAirport(org);
            pathDetail.setDestAirport(dest);

            Optional<Route> route = DataUtil.routes.parallelStream()
                    .filter(Objects::nonNull)
                    .filter(r -> r.getOrigin().equals(org.getIata()) && r.getDestination().equals(dest.getIata()))
                    .findFirst();

            if (route.isPresent()) {
                Optional<Airline> airline = DataUtil.airlines.parallelStream()
                        .filter(Objects::nonNull)
                        .filter(a -> a.getId().equals(route.get().getAirlineId()))
                        .findFirst();

                airline.ifPresent(pathDetail::setAirline);

            }

            details.add(pathDetail);
        }


        return details;
    }

    private static String getShortestPath(LinkedList<Airport> pathList) {
        StringBuilder shortestPath = new StringBuilder();
        for (Airport airport : pathList) {
            shortestPath.append(airport).append(" -> ");
        }

        return shortestPath.substring(0, shortestPath.toString().lastIndexOf('-') - 1);
    }

}
