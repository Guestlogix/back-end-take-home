import unittest

from app import create_app

class TestApi(unittest.TestCase):
  def setUp(self):
    self.app = create_app('test')

  def test_routes(self):
    with self.app.test_client() as client:
      result = client.get('/api/flights/route?origin=YEG&destination=GRU')
      self.assertEqual(result.status_code, 200)
