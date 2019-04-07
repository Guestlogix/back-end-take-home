import unittest
import csv

import neomodel

from app.models import Airport, FlightRoute
from app.tasks import SeedAirports, SeedRoutes, SeedAirlines

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
    commands = SeedAirlines()
    commands.run('data/airlines.csv')

  def test_seed_airports(self):
    commands = SeedAirports()
    commands.run('data/airports.csv')

  def test_seed_routes(self):
    command = SeedRoutes()
    command.run('data/routes.csv')

  def test_shortes_path_query(self):
    query_str = "MATCH (start:Airport {code:'YEG'}), (end:Airport {code:'GRU'}), \
      p = shortestPath((start)-[:DESTINATION*]->(end)) RETURN (p)"
    res, meta = neomodel.db.cypher_query(query_str)
    relations = [i for i in res[0][0]]
    self.assertEqual(len(relations), 2)
    self.assertEqual(relations[1]['arrives_to'], 'GRU')

