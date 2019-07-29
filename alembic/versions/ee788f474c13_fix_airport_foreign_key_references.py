"""Fix airport foreign key references

Revision ID: ee788f474c13
Revises: 2085d3382738
Create Date: 2019-07-25 21:59:21.723166

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = 'ee788f474c13'
down_revision = '2085d3382738'
branch_labels = None
depends_on = None


def upgrade():
    op.drop_constraint(constraint_name="route_destination_fkey", table_name="route", type_="foreignkey")
    op.drop_constraint(constraint_name="route_origin_fkey", table_name="route", type_="foreignkey")
    op.create_foreign_key(
        constraint_name="route_destination_fkey",
        source_table="route",
        referent_table="airport",
        local_cols=["destination"],
        remote_cols=["iata_three"])
    op.create_foreign_key(
        constraint_name="route_origin_fkey",
        source_table="route",
        referent_table="airport",
        local_cols=["origin"],
        remote_cols=["iata_three"])


def downgrade():
    op.drop_constraint(constraint_name="route_destination_fkey", table_name="route", type_="foreignkey")
    op.drop_constraint(constraint_name="route_origin_fkey", table_name="route", type_="foreignkey")
    op.create_foreign_key(
        constraint_name="route_destination_fkey",
        source_table="route",
        referent_table="airline",
        local_cols=["destination"],
        remote_cols=["iso_alpha_two"])
    op.create_foreign_key(
        constraint_name="route_origin_fkey",
        source_table="route",
        referent_table="airline",
        local_cols=["origin"],
        remote_cols=["iso_alpha_two"])
