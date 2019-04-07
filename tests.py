import unittest
from tests import TestSeed
from tests import TestApi
from tests import TestShortestPathQuery

if __name__ == '__main__':
  loader = unittest.TestLoader()
  suite = unittest.TestSuite()
  tests_to_run = [unittest.makeSuite(TestSeed), unittest.makeSuite(TestShortestPathQuery), unittest.makeSuite(TestApi)]
  suite.addTests(tests_to_run)
  runner = unittest.TextTestRunner()
  runner.run(suite)
