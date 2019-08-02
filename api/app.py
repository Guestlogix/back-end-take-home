from os import path;

from flask import Flask
from flask import request

import flightsDataLoader;
import routeFinder;

app = Flask(__name__)


data_dir = ".." + path.sep + "data" + path.sep + "full" + path.sep ;
flights_data = (None, None, None);


@app.route('/findRoute')
def findRoute():
	org = request.args.get('org')
	dst = request.args.get('dst')
	return routeFinder.findRoute(org,dst,flights_data);
	
if __name__ == '__main__':
	#load data
	flights_data = flightsDataLoader.load(data_dir);
	app.run(host="0.0.0.0", port=80)

