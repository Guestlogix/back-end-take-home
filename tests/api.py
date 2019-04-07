import unittest
import json

from app import create_app

class TestApi(unittest.TestCase):
  def setUp(self):
    self.app = create_app('test')

  def test_good_call(self):
    with self.app.test_client() as client:
      result = client.get('/api/airports/routings?origin=YEG&destination=GRU')
      self.assertEqual(result.status_code, 200)
      self.assertEqual(result.json['origin'], 'YEG')
      self.assertEqual(result.json['number_of_stops'], 1)
      self.assertEqual(len(result.json['routes']), 2)
      self.assertEqual(result.json['routes'][0]['origin_code'], 'YEG')
      self.assertEqual(result.json['routes'][1]['destination_code'], 'GRU')

  def test_bad_request(self):
    with self.app.test_client() as client:
      result = client.get('/api/airports/routings?destination=GRU')
      self.assertEqual(result.status_code, 400)
      self.assertTrue('Origin' in result.json['message'])

  def test_bad_airport(self):
    with self.app.test_client() as client:
      result = client.get('/api/airports/routings?origin=YEG&destination=GRA')
      self.assertEqual(result.status_code, 404)
      self.assertTrue('Airport' in result.json['message'])

  def test_bad_route(self):
    with self.app.test_client() as client:
      result = client.get('/api/airports/routings?origin=GIG&destination=DUH')
      self.assertEqual(result.status_code, 404)
      self.assertTrue('No route available between' in result.json['message'])
