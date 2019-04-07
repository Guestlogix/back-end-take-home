import os

class DevConfig:
    APP_NAME = os.environ.get('APP_NAME') or 'AppRouting'
    DATABASE_URL = os.environ.get('DB_URL') or 'bolt://neo4j:testpwd@localhost:7687'
    DEBUG=True

class TestConfig(DevConfig):
    TESTING=True

Config = {
  'development': DevConfig,
  'test': TestConfig,
  'default': DevConfig
}
