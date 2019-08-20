package com.Guestlogix.shortestRoute.controller;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.context.SpringBootTest.WebEnvironment;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import com.Guestlogix.shortestRoute.ShortestRouteApplication;
import com.Guestlogix.shortestRoute.domain.jpa.Airline;
import com.Guestlogix.shortestRoute.domain.network.response.DbListResponse;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(classes = ShortestRouteApplication.class)
@SpringBootTest(webEnvironment = WebEnvironment.RANDOM_PORT)
@AutoConfigureMockMvc
public class ShortestRouteControllerTest {

	@MockBean
	private ShortestRouteController shortestRouteController;

	@Autowired
	private TestRestTemplate template;

	@Before
	public void setup() throws Exception {

	}

	@Test
	public void invalidDestination() throws Exception {
		DbListResponse<Airline> resultAsset = template.getRestTemplate()
				.getForObject("http://localhost:5051/shortest-route?origin=YYZ&destination=AOA", DbListResponse.class);
		Assert.assertNotNull(resultAsset.getErrorCode().equalsIgnoreCase("Invalid Destination"));
	}

	@Test
	public void invalidOrigin() throws Exception {
		DbListResponse<Airline> resultAsset = template.getRestTemplate()
				.getForObject("http://localhost:5051/shortest-route?origin=AOA&destination=EWR", DbListResponse.class);
		Assert.assertNotNull(resultAsset.getErrorCode().equalsIgnoreCase("Invalid Origin"));
	}

	@Test
	public void validRoute() throws Exception {
		DbListResponse<Airline> resultAsset = template.getRestTemplate()
				.getForObject("http://localhost:5051/shortest-route?origin=YYZ&destination=YVR", DbListResponse.class);
		Assert.assertNotNull(resultAsset.getData());
	}

	private HttpEntity<Object> getHttpEntity(Object body) {
		HttpHeaders headers = new HttpHeaders();
		headers.setContentType(MediaType.APPLICATION_JSON);
		return new HttpEntity<Object>(body, headers);
	}
}
