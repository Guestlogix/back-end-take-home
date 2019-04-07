import os
from flask_script import Manager, Shell
from app import create_app
from app.tasks import SeedAirlines, SeedAirports,  SeedRoutes

app = create_app(os.getenv('FLASK_ENV') or 'development')
manager = Manager(app)

def make_shell_context():
  return dict(app=app)

manager.add_command('shell', Shell(make_context=make_shell_context))
manager.add_command('seed_airlines', SeedAirlines)
manager.add_command('seed_airports', SeedAirports)
manager.add_command('seed_routes', SeedRoutes)

@manager.command
def runserver():
    app.run(host='0.0.0.0', port=5000)

if __name__ == '__main__':
    manager.run()
