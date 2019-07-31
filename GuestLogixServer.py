"""
Simple tornado webserver to service route requests
"""

from tornado.web import Application, RequestHandler
from tornado.ioloop import IOLoop
import logging
import pandas as pd

ORIGIN = 'Origin'
DESTINATION = 'Destination'
log = logging.getLogger(__name__)


class GuestlogixRouteHandler(RequestHandler):
    """
    class to handle Routing requests
    """
    df_routes = pd.read_csv("./data/full/routes.csv")
    df_airlines = pd.read_csv("./data/full/airlines.csv")
    df_airports = pd.read_csv("./data/full/airports.csv")

    # dict of routes with origin as key and list of destnations as values
    route_map = {k: list(set(v[DESTINATION].tolist())) for k, v in df_routes[[ORIGIN, DESTINATION]].groupby(ORIGIN)}

    # Mega dataframe with Route->Airline->Airports info
    df = df_routes.merge(df_airlines.add_prefix('Airline - '), how='left', left_on='Airline Id',
                                right_on='Airline - 2 Digit Code')
    df = df.merge(df_airports.add_prefix('Origin - '), how='left', left_on='Origin',
                                right_on='Origin - IATA 3')
    df = df.merge(df_airports.add_prefix('Destination - '), how='left', left_on='Destination',
                                right_on='Destination - IATA 3')

    def get(self):
        """
        Returns a string containing the flight routing options
        Optionally with full detail in verbose mode
        """
        self.origin = self.get_argument(ORIGIN.lower(), None, True)
        self.destination = self.get_argument(DESTINATION.lower(), None, True)
        self.verbose = self.get_argument('verbose', None, True)  # Future impl to print flight & airport info

        log.info("Received request to find route from {} to {}".format(self.origin, self.destination))

        self.write(self.get_route())

    def get_route(self):
        """
        Returns route info - if available - in desired format
        """
        route_info = 'No Route'
        if self.origin is None or self.origin == '' or self.origin not in GuestlogixRouteHandler.df_airports[
            'IATA 3'].tolist():
            route_info = 'Invalid Origin'
        elif self.destination is None or self.destination == '' or self.destination not in \
                GuestlogixRouteHandler.df_airports['IATA 3'].tolist():
            route_info = 'Invalid Destination'
        else:
            route_info = GuestlogixRouteHandler.find_shortest_path(self.origin, self.destination)
            if isinstance(route_info, list):
                if not self.verbose:
                    route_info = ' -> '.join(route_info)
                else:
                    verbose_print = ''
                    for i in range(len(route_info)):
                        if i != len(route_info) - 1:
                            verbose_print += GuestlogixRouteHandler.df[
                                (GuestlogixRouteHandler.df['Origin'] == route_info[i]) & (
                                            GuestlogixRouteHandler.df['Destination'] == route_info[
                                        i + 1])].to_html(index = False)
                    route_info = verbose_print
        log.info("Route from {} to {} : {}".format(self.origin, self.destination, route_info))
        return route_info

    @staticmethod
    def find_shortest_path(start, end):
        """
        Classic Breath First Search algo

        :param start: origin
        :param end: destination
        :return: shortest path from last recursive iteration
        """
        marked = []
        queue = [[start]]

        if start == end:
            return 'No Route'  # 'Origin & Destination cannot be same place unless around the world trip'
        while queue:
            path = queue.pop(0)
            node = path[-1]
            if node not in marked and node in GuestlogixRouteHandler.route_map:
                destinations = GuestlogixRouteHandler.route_map[node]
                # go through all destination nodes, construct a new path and
                # push it into the queue
                for destination in destinations:
                    new_path = list(path)
                    new_path.append(destination)
                    queue.append(new_path)
                    # return path if destination is end
                    if destination == end:
                        return new_path
                # mark node as explored
                marked.append(node)
        return 'No Route'


if __name__ == '__main__':
    log.level = logging.INFO
    ch = logging.StreamHandler()
    ch.setLevel(logging.INFO)
    ch.setFormatter(logging.Formatter(fmt='%(asctime)s|%(funcName)s:%(lineno)d|%(levelname)s|%(message)s',
                                      datefmt='%H:%M:%S'))
    log.addHandler(ch)
    log.info("Starting Guestlogix RouteHandler")
    app = Application([("/", GuestlogixRouteHandler)])
    app.listen(4000)
    IOLoop.instance().start()
