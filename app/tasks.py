import csv
from flask_script import Command

from .models import Airport, Airline

class BaseCommand(Command):
  def __init__(self, fname):
    self._raw_data = self.__load_csv(fname)
    super().__init__(func=None)

  def __load_csv(self, fname):
    fh = open(fname, 'r')
    reader = csv.DictReader(fh)
    lines = [i for i in reader]
    fh.close()
    return lines

class SeedAirlines(BaseCommand):
  def run(self):
    for airline in self._raw_data:
      al = Airline(code2=airline['2 Digit Code'], name=airline['Name'],
                    code3=airline['3 Digit Code'],
                    country=airline['Country'])
      al.save()

class SeedAirports(BaseCommand):
  def run(self):
    for airport in self._raw_data:
      ap = Airport(code=airport['IATA 3'], name=airport['Name'], city=airport['City'],
                    country=airport['Country'], latitute=airport['Latitute '],
                    longitude=airport['Longitude'])
      ap.save()

class SeedRoutes(BaseCommand):
  def run(self):
    for route in self._raw_data:
      origin = self._get_or_create_airport(route['Origin'])
      destination = self._get_or_create_airport(route['Destination'])

      airline = self._get_or_create_airline(route['Airline Id'])

      flight_route = {'airline': airline.code2, 'departs_from':route['Origin'],
                        'arrives_to': route['Destination']}

      origin.destinations.connect(destination, flight_route)

  def _get_or_create_airport(self, code):
    airport = Airport.nodes.get_or_none(code=code)
    if not airport:
      airport = Airport(code=code, name='N/A', city='N/A', country='N/A')
      airport.save()
    return airport

  def _get_or_create_airline(self,code2):
    airline = Airline.nodes.get_or_none(code2=code2)
    if not airline:
      airline = Airline(code2=code2, name='N/A',code3='N/A', country='N/A')
      airline.save()
    return airline
