from flask import Blueprint, request, abort, make_response, jsonify
from flasgger import swag_from, validate
try:
  from http import HTTPStatus
except ImportError:
  import httplib as HTTPStatus
try:
  import simplejson as json
except ImportError:
  import json

from .queries import AirportNotFound, RouteNotFound, FlightConnectionsQuery

bp = Blueprint('api', __name__, url_prefix='/api')

@bp.route('flights/route', methods=('GET',))
@swag_from('resources/flight_route.yml')
def route_flights():
  if not request.args.get('origin'):
    abort(make_response(jsonify(message="Origin is required"), 400))
  if not request.args.get('destination'):
    abort(make_response(jsonify(message="Destination is required"), 400))

  query = FlightConnectionsQuery(request.args.get('origin'),
                                  request.args.get('destination'))
  try:
    data = query.perform()
  except AirportNotFound as ex:
    abort(make_response(jsonify(message="%s" % ex), 404))
  except RouteNotFound as ex:
    abort(make_response(jsonify(message="%s" % ex), 404))
  except Exception as ex:
    import traceback
    print(traceback.format_exc())
    template = "An exception of type {0} occurred. Arguments:\n{1!r}"
    message = template.format(type(ex).__name__, ex.args)
    abort(make_response(jsonify(message=message), 500))

  return make_response(jsonify([d.__dict__ for d in data]), 200)
