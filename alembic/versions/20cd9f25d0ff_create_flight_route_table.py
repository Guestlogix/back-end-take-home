"""Create flight route table

Revision ID: 20cd9f25d0ff
Revises: 1def77f85229
Create Date: 2019-07-10 22:35:50.022074

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '20cd9f25d0ff'
down_revision = '1def77f85229'
branch_labels = None
depends_on = None


def upgrade():
    op.create_index("idx_airport_iata_three", "airport", ["iata_three"], unique=True)
    op.create_index("idx_airline_iso_alpha_two", "airline", ["iso_alpha_two"], unique=True)
    op.create_table(
        "route",
        sa.Column("id", sa.Integer, primary_key=True),
        sa.Column("airline_id", sa.Text, sa.ForeignKey("airport.iata_three"), nullable=False),
        sa.Column("origin", sa.Text, sa.ForeignKey("airline.iso_alpha_two"), nullable=False),
        sa.Column("destination", sa.Text, sa.ForeignKey("airline.iso_alpha_two"), nullable=False),
    )


def downgrade():
    op.drop_index("idx_airport_iata_three")
    op.drop_index("idx_airline_iso_alpha_two")
    op.drop_table("route")
