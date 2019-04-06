from flask_script import Command

class Seed(Command):
  def __init__(self):
    super().__init__(func=None)

  def run(self):
    raise NotImplementedError
