import pytest


def test_shortest_valid_route(client):
    res = client.get("/?origin=YYZ&destination=JFK")

    assert res.status_code == 200
    assert res.json == {"status": "success", "data": ["YYZ", "JFK"]}

    res = client.get("/?origin=YYZ&destination=YVR")

    assert res.status_code == 200
    assert res.json == {"status": "success", "data": ["YYZ", "JFK", "LAX", "YVR"]}


def test_route_not_found(client):
    res = client.get("/?origin=YYZ&destination=ORD")
    assert res.status_code == 404
    assert res.json == {"status": "fail", "data": None}


def test_invalid_route(client):
    res = client.get("/?origin=XXX&destination=ORD")
    assert res.status_code == 400
    assert res.json == {"status": "fail", "data": "Invalid Origin"}

    res = client.get("/?origin=ORD&destination=XXX")
    assert res.status_code == 400
    assert res.json == {"status": "fail", "data": "Invalid Destination"}
