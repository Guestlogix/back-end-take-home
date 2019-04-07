from neomodel import (StructuredNode, StringProperty, FloatProperty,
    Relationship, RelationshipTo, RelationshipFrom, StructuredRel)

class Airline(StructuredNode):
  code2 = StringProperty(unique_index=True, required=True)
  code3 = StringProperty()
  name = StringProperty()
  country = StringProperty()
  # routes = RelationshipFrom('FlightRoute', 'OPERATED_BY', )

class FlightRoute(StructuredRel):
  # airline = RelationshipTo('Airline', 'OPERATED_BY')
  airline = StringProperty(required=True)
  departs_from = StringProperty(required=True)
  arrives_to = StringProperty(required=True)

class Airport(StructuredNode):
  code = StringProperty(unique_index=True, required=True)
  name = StringProperty()
  city = StringProperty()
  country = StringProperty()
  latitute = FloatProperty()
  longitude = FloatProperty()
  destinations = Relationship('Airport', 'DESTINATION', model=FlightRoute)
