import unittest
import csv

import neomodel

from app.models import Airport, FlightRoute

class TestSeed(unittest.TestCase):
  def load_csv(self, fname):
    fh = open(fname, 'r')
    reader = csv.DictReader(fh)
    lines = [i for i in reader]
    fh.close()
    return lines

  def setUp(self):
    neomodel.config.DATABASE_URL = 'bolt://neo4j:testpwd@localhost:7687'

  def tearDown(self):
    pass

  def test_seed_airlines(self):
    for airport in self.load_csv('data/airports.csv'):
      ap = Airport(code=airport['IATA 3'], name=airport['Name'], city=airport['City'],
                    country=airport['Country'], latitute=airport['Latitute '],
                    longitude=airport['Longitude'])
      ap.save()

  def test_seed_routes(self):
    for route in self.load_csv('data/routes.csv'):
      # flight = Flight(airport=route['Airline Id'])
      # flight.save()
      origin = Airport.nodes.get_or_none(code=route['Origin'])
      if not origin:
        origin = Airport(code=route['Origin'], name='N/A', city='N/A', country='N/A')
        origin.save()
      destination = Airport.nodes.get_or_none(code=route['Destination'])
      if not destination:
        destination = Airport(code=route['Destination'])
        destination.save()
      # flight.origin.connect(origin)
      # flight.destination.connect(destination)
      flight_route = {'airline':route['Airline Id'], 'departs_from':route['Origin'],
                                  'arrives_to': route['Destination']}
      origin.destinations.connect(destination, flight_route)

  def test_shortes_path_query(self):
    query_str = "MATCH (start:Airport {code:'YEG'}), (end:Airport {code:'GRU'}), \
      p = shortestPath((start)-[:DESTINATION*]-(end)) RETURN (p)"
    res, meta = neomodel.db.cypher_query(query_str)
    relations = [i for i in res[0][0]]
    self.assertEqual(len(relations), 2)
    self.assertEqual(relations[1]['arrives_to'], 'GRU')

