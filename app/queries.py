from typing import NamedTuple
from neomodel import db
from .models import Airport, FlightRoute, Airline

class RouteNotFound(Exception):
  pass

class AirportNotFound(Exception):
  pass

class BaseFlightConnectionsQueryResultItem(NamedTuple):
  route: FlightRoute
  airline: Airline
  start: Airport
  end: Airport

class FlightConnectionsQueryResultItem(BaseFlightConnectionsQueryResultItem):
  __dict__ = property(BaseFlightConnectionsQueryResultItem._asdict)

class FlightConnectionsQuery:
  def __init__(self, origin_code, destination_code):
    self._origin_code = origin_code
    self._destination_code = destination_code

  def perform(self):
    if not Airport.nodes.get_or_none(code=self._origin_code):
      raise AirportNotFound("Airport {0} not found".format(self._origin_code))
    if not Airport.nodes.get_or_none(code=self._destination_code):
      raise AirportNotFound('Airport {0} not found'.format(self._destination_code))
    res, meta =  self._execute()

    if len(res) and len(res[0]):
      return [
        FlightConnectionsQueryResultItem(route=FlightRoute.inflate(i),
          # This is ineficiente, but will work while I don't figure out how to make the relationship
          airline=Airline.nodes.get_or_none(code2=i['airline']),
          start=Airport.inflate(i.start_node),
          end=Airport.inflate(i.end_node)) for i in res[0][0]
      ]
    else:
      raise RouteNotFound('No route available between {0} and {1}'.format(self._origin_code, self._destination_code))

  def _execute(self):
    query_str = "MATCH (start:Airport {code:'%s'}), (end:Airport {code:'%s'}), \
      p = shortestPath((start)-[:DESTINATION*]->(end)) RETURN (p)"

    return db.cypher_query(query_str % (self._origin_code, self._destination_code))


