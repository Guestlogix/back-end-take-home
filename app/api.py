from flask import Blueprint, request, abort, make_response, jsonify
from flasgger import swag_from, validate
from mapper.object_mapper import ObjectMapper

from .queries import (AirportNotFound, RouteNotFound, FlightConnectionsQuery,
                        FlightConnectionsQueryResultItem)
from .entities import (CompactRouteEntity, QueryResultItemToCompactRouteEntity,
                        RouteSearchResult)

bp = Blueprint('api', __name__, url_prefix='/api')
mapper = ObjectMapper()
mapper.create_map(FlightConnectionsQueryResultItem, CompactRouteEntity, QueryResultItemToCompactRouteEntity)

@bp.route('health', methods=('GET',))
def health_check():
  """Checks api health
  ---
  produces:
    text/html
  responses:
    200:
      description: Ok
  """
  return make_response('Server is Up', 200)

@bp.route('routes', methods=('GET',))
@swag_from('resources/flight_route.yml')
def route_flights():
  origin = request.args.get('origin')
  destination = request.args.get('destination')
  if not origin:
    abort(make_response(jsonify(message="Origin is required"), 400))
  if not destination:
    abort(make_response(jsonify(message="Destination is required"), 400))

  query = FlightConnectionsQuery(origin.upper(), destination.upper())
  try:
    data = query.perform()
  except AirportNotFound as ex:
    abort(make_response(jsonify(message="%s" % ex), 404))
  except RouteNotFound as ex:
    abort(make_response(jsonify(message="%s" % ex), 404))
  except Exception as ex:
    import traceback
    print(traceback.format_exc())
    template = "An unexpect error of type {0} occurred. Details :\n{1!r}"
    message = template.format(type(ex).__name__, ex.args)
    abort(make_response(jsonify(message=message), 500))

  routes = [mapper.map(d, CompactRouteEntity) for d in data]
  response_data = RouteSearchResult(origin=origin, destination=destination, number_of_stops=len(routes)-1,
                                      routes=routes)

  return make_response(jsonify(response_data.encode()), 200)
