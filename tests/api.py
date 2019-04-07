import unittest

from app import create_app

class TestApi(unittest.TestCase):
  def setUp(self):
    self.app = create_app('test')

  def test_good_call(self):
    with self.app.test_client() as client:
      result = client.get('/api/airports/routings?origin=YEG&destination=GRU')
      self.assertEqual(result.status_code, 200)

  def test_bad_request(self):
    with self.app.test_client() as client:
      result = client.get('/api/airports/routings?destination=GRU')
      self.assertEqual(result.status_code, 400)

  def test_bad_airport(self):
    with self.app.test_client() as client:
      result = client.get('/api/airports/routings?origin=YEG&destination=GRA')
      self.assertEqual(result.status_code, 404)
