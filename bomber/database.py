import os

from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import scoped_session, sessionmaker

pg_user = os.getenv("BOMBER_PG_USER", "postgres")
pg_pass = os.getenv("BOMBER_PG_PASSWORD", "foobar")
pg_host = os.getenv("BOMBER_PG_HOST", "localhost")
pg_db = os.getenv("BOMBER_PG_DB", "postgres")

engine = create_engine(
    (f"postgresql+psycopg2://{pg_user}:{pg_pass}@{pg_host}/{pg_db}"),
    convert_unicode=True,
    echo=bool(os.getenv("BOMBER_DEBUG")),
    pool_pre_ping=True,
)
"""
> [Setting pool_pre_ping] adds a small bit of overhead to the connection checkout process, however is otherwise the most simple and
> reliable approach to completely eliminating database errors due to stale pooled connections. The calling application
> does not need to be concerned about organizing operations to be able to recover from stale connections checked out from
> the pool.

From http://docs.sqlalchemy.org/en/latest/core/pooling.html#disconnect-handling-pessimistic:
"""


db_session = scoped_session(sessionmaker(bind=engine))
Base = declarative_base()
Base.query = db_session.query_property()


def init_db():
    # import all modules here that might define models so that
    # they will be registered properly on the metadata.  Otherwise
    # you will have to import them first before calling init_db()
    import bomber.models

    Base.metadata.create_all(bind=engine)
