"""Fix airline_id foreign key reference

Revision ID: 2085d3382738
Revises: 567fb41e727b
Create Date: 2019-07-25 21:39:10.291091

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '2085d3382738'
down_revision = '567fb41e727b'
branch_labels = None
depends_on = None


def upgrade():
    op.drop_constraint(constraint_name="route_airline_id_fkey", table_name="route", type_="foreignkey")
    op.create_foreign_key(
        constraint_name="route_airline_id_fkey",
        source_table="route",
        referent_table="airline",
        local_cols=["airline_id"],
        remote_cols=["iso_alpha_two"])


def downgrade():
    op.drop_constraint(constraint_name="route_airline_id_fkey", table_name="route", type_="foreignkey")
    op.create_foreign_key(
        constraint_name="route_airline_id_fkey",
        source_table="route",
        referent_table="airport",
        local_cols=["airline_id"],
        remote_cols=["iata_three"])