"""Add missing column to airport table

Revision ID: 1def77f85229
Revises: 80606128b017
Create Date: 2019-07-10 22:32:34.042064

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '1def77f85229'
down_revision = '80606128b017'
branch_labels = None
depends_on = None


def upgrade():
    op.add_column("airport", sa.Column('longitude', sa.Numeric))


def downgrade():
    with op.batch_alter_table("airport") as batch_op:
        batch_op.drop_column('longitude')
