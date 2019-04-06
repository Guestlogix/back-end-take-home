from neomodel import (StructuredNode, StringProperty, IntegerProperty,
    Relationship, RelationshipFrom, RelationshipTo)

class Airport(StructuredNode):
  code = StringProperty(unique_index=True, required=True)
  destinations = RelationshipFrom('Flight', 'DESTINATION')

class Flight(StructuredNode):
  airline = StringProperty(equired=True)
  origin = RelationshipTo(Airport, 'ORIGIN')
  destination = RelationshipTo(Airport, 'DESTINATION')
