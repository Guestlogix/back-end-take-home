"""Create airport table

Revision ID: 80606128b017
Revises: 0f8752ded883
Create Date: 2019-07-10 22:23:15.413983

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '80606128b017'
down_revision = '0f8752ded883'
branch_labels = None
depends_on = None


def upgrade():
    op.create_table(
        "airport",
        sa.Column("id", sa.Integer, primary_key=True),
        sa.Column("name", sa.Text),
        sa.Column("city", sa.Text),
        sa.Column("country", sa.Text),
        sa.Column("iata_three", sa.Text),
        sa.Column("latitude", sa.Numeric),
    )


def downgrade():
    op.drop_table("airport")
