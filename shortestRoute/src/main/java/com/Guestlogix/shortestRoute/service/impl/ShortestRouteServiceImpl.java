package com.Guestlogix.shortestRoute.service.impl;

import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.stream.Collectors;

import org.apache.commons.lang3.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.util.CollectionUtils;

import com.Guestlogix.shortestRoute.domain.jpa.Airline;
import com.Guestlogix.shortestRoute.domain.jpa.Route;
import com.Guestlogix.shortestRoute.domain.network.response.DbListResponse;
import com.Guestlogix.shortestRoute.exceptions.ApiExCode;
import com.Guestlogix.shortestRoute.exceptions.ApiException;
import com.Guestlogix.shortestRoute.repository.jpa.AirlineRepository;
import com.Guestlogix.shortestRoute.repository.jpa.RouteRepository;
import com.Guestlogix.shortestRoute.service.ShortestRouteService;
import com.Guestlogix.shortestRoute.util.ShortestPathUtil;

/**
 * 
 * @author nchopra
 *
 */
@Service
public class ShortestRouteServiceImpl implements ShortestRouteService {

	@Autowired
	private RouteRepository routeRepository;

	@Autowired
	private AirlineRepository airlineRepository;

	@Override
	public DbListResponse<Airline> findConnectingAirlines(String origin, String destination) throws ApiException {
		DbListResponse<Airline> resp = new DbListResponse<Airline>();

		if (CollectionUtils.isEmpty(routeRepository.findByOrigin(origin))) {
			throw new ApiException(ApiExCode.ORIGIN_NOT_FOUND.getCode(), ApiExCode.ORIGIN_NOT_FOUND.getMessage());
		} else if (CollectionUtils.isEmpty(routeRepository.findByDestination(destination))) {
			throw new ApiException(ApiExCode.DESTINATION_NOT_FOUND.getCode(),
					ApiExCode.DESTINATION_NOT_FOUND.getMessage());
		} else {
			Set<String> airlineIds = new HashSet<String>(0);
			List<Route> routes = routeRepository.findAll();
			Map<String, String> routeAirlineMap = new HashMap<String, String>();
			for (Route r : routes) {
				ShortestPathUtil.addEdge(r.getOrigin(), r.getDestination());
				routeAirlineMap.put(r.getOrigin() + "~" + r.getDestination(), r.getAirline_id());
			}
			List<String> shortestPath = ShortestPathUtil.evaluateShortesPath(origin, destination);
			if (!CollectionUtils.isEmpty(shortestPath)) {
				for (int index = 0; index < shortestPath.size()-1; index++) {
					String airlineCode = routeAirlineMap.get(shortestPath.get(index) + "~" + shortestPath.get(index+1));
					if (StringUtils.isNotBlank(airlineCode))
						airlineIds.add(airlineCode);
				}
				List<Airline> airlines = airlineRepository
						.findBytwodigitCodes(airlineIds.stream().collect(Collectors.toList()));
				resp.setData(airlines);
			} else {
				throw new ApiException(ApiExCode.NO_ROUTE.getCode(), ApiExCode.NO_ROUTE.getMessage());
			}
		}
		resp.setStatus(1);
		return resp;
	}

}
