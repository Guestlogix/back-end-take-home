# Prerequisites

1- NodeJS 10+
2- NPM 3.5+
3- Port 3000 not being used
4- Please ensure the data folder and its contents are present

# Install Instructions

1- npm install
2- npm start

# Test Instructions

1- npm install
2- npm test

# Endpoint

GET /?origin={orgin}&destination={destination} => Get the shortest path summary and details of airlines and airports involved
E.g localhost:3000/?origin=YYZ&destination=JFK


# Example success response

{
    "summary": "YYZ=>JFK",
    "details": [
        {
            "path": [
                "YYZ",
                "JFK"
            ],
            "AirlineDetails": [
                {
                    "Airline Id": "AC",
                    "Origin": "YYZ",
                    "Destination": "JFK",
                    "details": [
                        {
                            "Name": "Air Canada",
                            "2 Digit Code": "AC",
                            "3 Digit Code": "ACA",
                            "Country": "Canada"
                        }
                    ]
                },
                {
                    "Airline Id": "WS",
                    "Origin": "YYZ",
                    "Destination": "JFK",
                    "details": [
                        {
                            "Name": "WestJet",
                            "2 Digit Code": "WS",
                            "3 Digit Code": "WJA",
                            "Country": "Canada"
                        }
                    ]
                }
            ],
            "AirportDetails": [
                [
                    {
                        "Name": "Lester B. Pearson International Airport",
                        "City": "Toronto",
                        "Country": "Canada",
                        "IATA 3": "YYZ",
                        "Latitute": "43.67720032",
                        "Longitude": "-79.63059998"
                    }
                ],
                [
                    {
                        "Name": "John F Kennedy International Airport",
                        "City": "New York",
                        "Country": "United States",
                        "IATA 3": "JFK",
                        "Latitute": "40.63980103",
                        "Longitude": "-73.77890015"
                    }
                ]
            ]
        }
    ]
}

#Contact Info

Please feel free to reach out to hasnain.shahzeb@gmail.com for any question or queries.

# Personal Note

Tried to make it to as simple as possible wihout the use of database.
But there are certainly alot of place for improvements.
