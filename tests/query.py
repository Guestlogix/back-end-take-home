import unittest
import neomodel
from app.models import Airport, FlightRoute
from app.queries import FlightConnectionsQuery

class TestShortestPathQuery(unittest.TestCase):
  def setUp(self):
    neomodel.config.DATABASE_URL = 'bolt://neo4j:testpwd@localhost:7687'

  def test_simple_case(self):
    query = FlightConnectionsQuery('AKL', 'GRU')
    res = query.perform()
    for r in res:
      print(r.__dict__)
    self.assertEqual(res[0].departs_from, 'AKL')
    self.assertEqual(res[-1].arrives_to, 'GRU')

  def test_cypher_result(self):
    query = FlightConnectionsQuery('AKL', 'GRU')
    res, meta = query._execute()

    last_code = 'AKL'
    for i in res[0][0]:
      self.assertEqual(i.start_node['code'], last_code)
      last_code = i.end_node['code']

