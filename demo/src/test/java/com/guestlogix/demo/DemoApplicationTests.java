package com.guestlogix.demo;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.guestlogix.demo.web.ResponseBean;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.core.io.ClassPathResource;
import org.springframework.http.*;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.web.client.RestTemplate;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.util.List;
import java.util.stream.Stream;

import static java.util.stream.Collectors.toList;
import static org.junit.Assert.assertEquals;

@RunWith(SpringRunner.class)
@SpringBootTest
public class DemoApplicationTests {

    private final ObjectMapper OBJECT_MAPPER = new ObjectMapper();
    public static final String BASE_URL = "http://localhost:8000";

    @Test
    public void contextLoads() {

        ClassPathResource resource = new ClassPathResource("testcases/case1.json");
        List<String> jsonStrings;
        try (InputStream inputStream = resource.getInputStream()) {
            jsonStrings =
                    new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8))
                            .lines().collect(java.util.stream.Collectors.toList());
        } catch (IOException ex) {
            System.out.println(String.join("\n", Stream.of(ex.getStackTrace())
                    .map(StackTraceElement::toString)
                    .collect(toList())));

            throw new Error(ex.toString());
        }


        if (!jsonStrings.isEmpty()) {
            jsonStrings.forEach(jsonString -> {

                try {
                    JsonNode jsonObject = OBJECT_MAPPER.readTree(jsonString);

                    JsonNode request = jsonObject.get("request");
                    JsonNode response = jsonObject.get("response");

                    String url = request.get("url").asText();
                    String statusCode = response.get("status_code").asText();


                    try {

                        String requestUrl = BASE_URL + url;

                        HttpHeaders headers = new HttpHeaders();
                        headers.set("Accept", MediaType.APPLICATION_JSON_VALUE);

                        HttpEntity<?> entity = new HttpEntity<>(headers);

                        RestTemplate restTemplate = new RestTemplate();
                        ResponseEntity<ResponseBean> httpResponse = restTemplate.exchange(
                                requestUrl,
                                HttpMethod.GET,
                                entity,
                                new ParameterizedTypeReference<ResponseBean>() {
                                });


                        if (validateStatusCode(httpResponse.getStatusCode().value(), statusCode)) {

                            JsonNode expectedType = response.get("headers").get("Content-Type");

                            if (expectedType != null && httpResponse.getHeaders().get("Content-Type") != null) {
                                validateContentType(httpResponse.getHeaders().get("Content-Type").get(0),
                                        expectedType.toString());
                            }

                            JsonNode expectedResponseJson = OBJECT_MAPPER.readValue(response.toString(),
                                    new TypeReference<JsonNode>() {
                                    });

                            ResponseBean responseBean = httpResponse.getBody();

                            validateJsonResponse(responseBean, expectedResponseJson);

                        }

                    } catch (Exception ex) {
                        System.out.println(String.join("\n", Stream.of(ex.getStackTrace())
                                .map(StackTraceElement::toString)
                                .collect(toList())));

                        throw new Error(ex.toString());
                    }

                } catch (IOException ex) {
                    System.out.println(String.join("\n", Stream.of(ex.getStackTrace())
                            .map(StackTraceElement::toString)
                            .collect(toList())));

                    throw new Error(ex.toString());
                }
            });
        }

    }

    private void validateJsonResponse(ResponseBean responsebean, JsonNode expectedResponseJson) {
        String expectedPath = expectedResponseJson.get("body").get("shortestPath").asText();
        String responsePath = responsebean.getShortestPath();

        if (responsePath != null && expectedPath != null) {

            assertEquals(responsePath, expectedPath);
        } else {
            String expectedMessage = expectedResponseJson.get("body").get("message").asText();
            String responseMessage = responsebean.getMessage();
            assertEquals(responseMessage, expectedMessage);

        }

    }

    private boolean validateStatusCode(int statusCode, String code) {
        return statusCode == Integer.valueOf(code);
    }

    private void validateContentType(String mimeType, String jsonMimeType) {
        assertEquals(mimeType, jsonMimeType.substring(1, jsonMimeType.length() - 1));

    }

}
