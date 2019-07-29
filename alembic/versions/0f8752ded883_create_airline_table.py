"""Create airline table

Revision ID: 0f8752ded883
Revises: 
Create Date: 2019-07-10 22:12:41.882821

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = "0f8752ded883"
down_revision = None
branch_labels = None
depends_on = None


def upgrade():
    op.create_table(
        "airline",
        sa.Column("id", sa.Integer, primary_key=True),
        sa.Column("name", sa.Text),
        sa.Column("iso_alpha_two", sa.TEXT),
        sa.Column("iso_three", sa.TEXT),
        sa.Column("country", sa.TEXT),
    )


def downgrade():
    op.drop_table("airline")
