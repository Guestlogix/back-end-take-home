from booby import Model, fields

class AirportEntity(Model):
  code = fields.String()
  name = fields.String()
  city = fields.String()
  country = fields.String()

class Airline(Model):
  code2 = fields.String()
  code3 = fields.String()
  name = fields.String()
  country = fields.String()

# class RouteEntity(Model):
#   origin = fields.Embedded(AirportEntity)
#   destination_code = fields.Embedded(AirportEntity)
#   airline = fields.Embedded(Airline)

class CompactRouteEntity(Model):
  origin_code = fields.String()
  origin_name = fields.String()
  destination_code = fields.String()
  destination_name = fields.String()
  airline_code = fields.String()
  airline_name = fields.String()

class RouteSearchResult(Model):
  origin = fields.String()
  destination = fields.String()
  number_of_stops = fields.Integer()
  routes = fields.Collection(CompactRouteEntity)

QueryResultItemToCompactRouteEntity = {
  'origin_code': lambda x: x.start.code,
  'origin_name': lambda x: x.start.name,
  'destination_code': lambda x: x.end.code,
  'destination_name': lambda x: x.end.name,
  'airline_code': lambda x: x.airline.code2,
  'airline_code3': lambda x: x.airline.code3,
  'airline_name': lambda x: x.airline.name,
}
