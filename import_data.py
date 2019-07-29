import argparse
import csv
import os
from enum import Enum

import psycopg2


class Table(Enum):
    AIRLINE = "airline"
    AIRPORT = "airport"
    ROUTE = "route"


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("data_file")
    parser.add_argument("table_name")
    args = parser.parse_args()

    if not os.path.isfile(args.data_file):
        raise TypeError(f"No file exists by the name '{args.data_file}'")

    if args.table_name not in {
        Table.AIRLINE.value,
        Table.AIRPORT.value,
        Table.ROUTE.value,
    }:
        raise ValueError("Unsupported table")

    conn = psycopg2.connect(
        user=os.getenv("BOMBER_PG_USER", "postgres"),
        host=os.getenv("BOMBER_PG_HOST", "localhost"),
        password=os.getenv("BOMBER_PG_PASSWORD", "foobar"),
        dbname=os.getenv("BOMBER_PG_DB", "postgres"),
        port=os.getenv("BOMBER_PG_PORT", "5432"),
    )
    try:
        with conn:
            with conn.cursor() as cur:
                with open(args.data_file) as f:
                    next(f)  # Skip first line containing column names
                    if args.table_name == Table.AIRLINE.value:
                        cur.copy_from(
                            f,
                            args.table_name,
                            sep=",",
                            columns=(
                                "name",
                                "iso_alpha_two",
                                "iso_alpha_three",
                                "country",
                            ),
                        )
                    elif args.table_name == Table.AIRPORT.value:
                        cur.copy_from(
                            f,
                            args.table_name,
                            sep=",",
                            columns=(
                                "name",
                                "city",
                                "country",
                                "iata_three",
                                "latitude",
                                "longitude",
                            ),
                        )

                    elif args.table_name == Table.ROUTE.value:
                        cur.copy_from(
                            f,
                            args.table_name,
                            sep=",",
                            columns=("airline_id", "origin", "destination"),
                        )

    except psycopg2.DataError:  # Deals with delimiter being in the middle of a quoted column value
        with conn:
            with conn.cursor() as cur:
                with open(args.data_file) as f:
                    reader = csv.DictReader(f, delimiter=",", skipinitialspace=True)
                    for row in reader:
                        if args.table_name == Table.AIRLINE.value:
                            cur.execute(
                                """INSERT INTO public.airline(
                                             name
                                             , iso_alpha_two
                                             , iso_alpha_three
                                             , country) 
                                           VALUES (
                                             %s
                                             , %s
                                             , %s
                                             , %s
                                           )""",
                                (
                                    row["Name"],
                                    row["2 Digit Code"],
                                    row["3 Digit Code"],
                                    row["Country"],
                                ),
                            )
                        elif args.table_name == Table.AIRPORT.value:
                            cur.execute(
                                """INSERT INTO public.airport(
                                             name
                                             , city
                                             , country
                                             , iata_three
                                             , latitude
                                             , longitude
                                           ) VALUES (
                                             %s
                                             , %s
                                             , %s
                                             , %s
                                             , %s
                                             , %s
                                           ) 
                                           """,
                                (
                                    row["Name"],
                                    row["City"],
                                    row["Country"],
                                    row["IATA 3"],
                                    row["Latitute"],  # Ed: sic
                                    row["Longitude"],
                                ),
                            )
                        elif args.table_name == Table.ROUTE.value:
                            cur.execute(
                                """INSERT INTO public.route(
                                             airline_id
                                             , origin
                                             , destination) 
                                           VALUES (
                                             %s
                                             , %s
                                             , %s                                             
                                           ) 
                                           """,
                                (row["Airline Id"], row["Origin"], row["Destination"]),
                            )


if __name__ == "__main__":
    main()
