step 1. Technologies used in the making
      -Github(Node js) for the coding and data transformation 
      -Heroku (mongodb) for the collection and storage of data 
      -postman or google chrome browser for output purpose.
      
step 2. How to see the code 
       - app.js is the main code file where all the coding reside.
       - As Heroku have the direct connection to the github platform we will deploy the app from here.
       
step 3. How to run the app
       - There are two ways to run the app either the web browser or postman app 
       -  web browser all you need to do in enter this web address ("https://mighty-depths-86733.herokuapp.com/BOM-YYZ") 
       where BOM is the origin and the YYZ is the distination. output will be like 
       {"shortestRoute":[{"_id":"5a3ab6687c59c84a43f9e848","airlineId":"TK","origin":"BOM","destination":"IST","distance":4823286},
       {"_id":"5a3ab6687c59c84a43f9e961","airlineId":"TK","origin":"IST","destination":"KZR","distance":235408},
       {"_id":"5a3ab6687c59c84a43f9e9d7","airlineId":"TK","origin":"KZR","destination":"YYZ","distance":235408}],
       "shortestDistance":5294102}
       also
       - we can use postman app in the GET Section enter this web address ("https://mighty-depths-86733.herokuapp.com/BOM-YYZ")and
       press SEND we can then see the output  like 
       {
    "shortestRoute": [
        {
            "_id": "5a3ab6687c59c84a43f9e848",
            "airlineId": "TK",
            "origin": "BOM",
            "destination": "IST",
            "distance": 4823286
        },
        {
            "_id": "5a3ab6687c59c84a43f9e961",
            "airlineId": "TK",
            "origin": "IST",
            "destination": "KZR",
            "distance": 235408
        },
        {
            "_id": "5a3ab6687c59c84a43f9e9d7",
            "airlineId": "TK",
            "origin": "KZR",
            "destination": "YYZ",
            "distance": 235408
        }
    ],
    "shortestDistance": 5294102
}

note: orgin and distination should always be in capital or exactly as it is in the data collections. 
