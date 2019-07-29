import os

import sentry_sdk
from flask import Flask, jsonify, request
from sentry_sdk.integrations.flask import FlaskIntegration
from werkzeug.exceptions import BadRequestKeyError

from bomber.database import db_session

# For real-time reporting
sentry_sdk.init(
    dsn=os.getenv(
        "BOMBER_SENTRY_KEY",
        "https://db97f87b50a14685b931fe43863e91ba@sentry.io/1501195",
    ),
    integrations=[FlaskIntegration()],
)


def create_app():
    api = Flask(__name__)
    api.secret_key = os.getenv(
        "BOMBER_SECRET_KEY", os.urandom(12).hex()
    )  # Change this in production!

    @api.errorhandler(Exception)
    def handle_generic_error(e):
        sentry_sdk.capture_exception(e)
        return jsonify({"status": "error", "message": str(e)}), 500

    @api.route("/")
    def get_routes():
        query_params = {}

        for arg in ("origin", "destination"):
            try:
                query_params[arg] = request.args[arg]
            except BadRequestKeyError:
                return (
                    jsonify(
                        {
                            "status": "fail",
                            "data": {arg: f"'{arg}' is a required query parameter."},
                        }
                    ),
                    400,
                )

        result = db_session.execute(
            """SELECT 
                   EXISTS(
                       SELECT 
                           1 
                       FROM 
                           airport 
                       WHERE 
                           iata_three = :origin ) AS origin_exists
                   , EXISTS(
                       SELECT 
                           1 
                       FROM 
                           airport 
                       WHERE
                           iata_three = :destination ) AS destination_exists""",
            query_params,
        )
        origin_exists, destination_exists = result.fetchone()

        if not origin_exists:
            return jsonify({"status": "fail", "data": "Invalid Origin"}), 400
        if not destination_exists:
            return jsonify({"status": "fail", "data": "Invalid Destination"}), 400

        result = db_session.execute(
            """WITH RECURSIVE flight_connection (src, dest, path) AS (
                   SELECT origin
                        , destination
                        , ARRAY [origin]
                   FROM route
                   UNION ALL
                   SELECT c.src
                        , f.destination
                        , c.path || f.origin
                   FROM route f
                            JOIN flight_connection c ON f.origin = c.dest
                   WHERE NOT f.origin = ANY (c.path) -- prevents infinite looping by not visiting previously visited nodes
                     AND NOT :destination = ANY (c.path) -- stops iteration when the destination is reached
               )
               SELECT *
               FROM flight_connection
               WHERE src = :origin
                   AND dest = :destination
               LIMIT 1  -- The recursive query evaluation algorithm produces its output in breadth-first search order. 
               ;""",
            query_params,
        )

        row = result.fetchone()
        if row is None:
            return jsonify({"status": "fail", "data": None}), 404

        origin, destination, connections = row

        flight_path = connections + [destination]

        return jsonify({"status": "success", "data": flight_path})

    @api.teardown_appcontext
    def shutdown_session(exception=None):
        """Automatically remove database sessions at the end of the
        request or when the application shuts down.

        :param exception: Exception :return: """
        db_session.remove()

    return api
