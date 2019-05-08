# GuestLogix Flight API 

## Introduction
This solution was built using the BFS(Breath First Search Algorithm) to:
- Fetch Connecting Flights between a given origin and destination in the query params
- Give user feedback when a given origin or destination does not exist
- Give user feedback when no route is found between a given origin and destination.

## Getting Started
To run this application locally, simply follow the following steps:
- Clone the repo
- Install dependencies by running `npm install --save`
- Run `node migration.js` to seed your local db
- Wait for about 30 seconds for the above process to complete
- Clear your terminal and run `npm start`
- The server is running live on: `localhost:3000`

## Testing the API local
- Go to Postman or your browser and pass in the url: `localhost:3000/api/routes?origin=XXX&destination=YYY`
- XXX and YYY represent the 'IATA 3' code for your origin and destination respectively.
- A sample test with `http://localhost:3000/api/routes?origin=ABJ&destination=IST` will return:

```
{
  "message": "Found shortest route",
  "airports": [
      [
          {
              "_id": "5cd21426c908350695497615",
              "name": "Port Bouet Airport",
              "city": "Abidjan",
              "country": "Cote d'Ivoire",
              "iata3": "ABJ",
              "longitude": "-3.926290035",
              "__v": 0
          }
      ],
      [
          {
              "_id": "5cd21427c908350695497b92",
              "name": "Atatürk International Airport",
              "city": "Istanbul",
              "country": "Turkey",
              "iata3": "IST",
              "longitude": "28.81459999",
              "__v": 0
          }
      ]
  ]
}
```

## Testing the API online
- Go to Postman or your browser and pass in the url: `https://guestlogix-flights.herokuapp.com/api/routes?origin=XXX&destination=YYY`
- XXX and YYY represent the 'IATA 3' code for your origin and destination respectively.
- A sample test with `https://guestlogix-flights.herokuapp.com/api/routes?origin=ABJ&destination=TEN` will return:

```
{
  "message": "Found shortest route",
  "airports": [
      [
          {
              "_id": "5cd2bc4b44dc981b60744e12",
              "name": "Port Bouet Airport",
              "city": "Abidjan",
              "country": "Cote d'Ivoire",
              "iata3": "ABJ",
              "longitude": "-3.926290035",
              "__v": 0
          }
      ],
      [
          {
              "_id": "5cd2bc4c44dc981b6074538f",
              "name": "Atatürk International Airport",
              "city": "Istanbul",
              "country": "Turkey",
              "iata3": "IST",
              "longitude": "28.81459999",
              "__v": 0
          }
      ],
      [
          {
              "_id": "5cd2bc4c44dc981b60745972",
              "name": "Guangzhou Baiyun International Airport",
              "city": "Guangzhou",
              "country": "China",
              "iata3": "CAN",
              "longitude": "113.2990036",
              "__v": 0
          }
      ],
      [
          {
              "_id": "5cd2bc4d44dc981b60746196",
              "name": "Tongren Fenghuang Airport",
              "city": "Tongren",
              "country": "China",
              "iata3": "TEN",
              "longitude": "109.308889",
              "__v": 0
          }
      ]
  ]
}
```

## Testing the API online
To run the test, simply run `npm test`

### Few things to Note:
There were some incomplete data set. E.g BKG is a route but has no corresponding airport in the airports.csv file.

