from neomodel import (StructuredNode, StringProperty, FloatProperty,
    Relationship, StructuredRel)

class FlightRoute(StructuredRel):
  airline = StringProperty(equired=True)
  departs_from = StringProperty(equired=True)
  arrives_to = StringProperty(equired=True)

class Airport(StructuredNode):
  code = StringProperty(unique_index=True, required=True)
  name = StringProperty()
  city = StringProperty()
  country = StringProperty()
  latitute = FloatProperty()
  longitude = FloatProperty()
  destinations = Relationship('Airport', 'DESTINATION', model=FlightRoute)
