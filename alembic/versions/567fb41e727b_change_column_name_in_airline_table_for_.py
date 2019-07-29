"""Change column name in airline table for better consistency

Revision ID: 567fb41e727b
Revises: 20cd9f25d0ff
Create Date: 2019-07-12 00:22:59.583034

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '567fb41e727b'
down_revision = '20cd9f25d0ff'
branch_labels = None
depends_on = None


def upgrade():
    op.alter_column(
        "airline",
        "iso_three",
        new_column_name="iso_alpha_three",
    )


def downgrade():
    op.alter_column(
        "airline",
        "iso_alpha_three",
        new_column_name="iso_three",
    )
