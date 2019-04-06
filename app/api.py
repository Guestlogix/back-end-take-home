from flask import Blueprint, request
from flasgger import swag_from

bp = Blueprint('api', __name__, url_prefix='/api')

@bp.route('flights/route', methods=('GET',))
@swag_from('../resources/flight_route.yml', validation=True, data= lambda: request.args)
def route_flights():
  raise NotImplementedError
