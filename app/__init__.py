from flask import Flask
import neomodel
from app.config import Config
from app.api import bp as api
from flasgger import Swagger

def create_app(config_name='default'):
  app = Flask(__name__)
  swagger = Swagger(app)
  app.register_blueprint(api)
  app.config.from_object(Config[config_name])
  neomodel.config.DATABASE_URL = Config[config_name].DATABASE_URL

  return app
