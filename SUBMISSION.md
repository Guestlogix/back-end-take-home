# bomber

*bomber* is a typical Flask REST API set up to serve flight routes
from a PostgreSQL database.

## Getting Started

### Vagrant

Host OS Requirements:
- [VirtualBox](https://www.virtualbox.org/)
- [Vagrant](https://www.vagrantup.com/)
- [Ansible](https://www.ansible.com/) (Make sure you get the latest
  version, just to be safe.)

```shell
git clone https://github.com/nu11h3r035/back-end-take-home
cd back-end-take-home
vagrant up
```

### Manual Setup

Requirements:
- [docker](https://www.docker.com/) or [podman](https://podman.io/)
- [Python 3.7](https://github.com/pyenv/pyenv)
- [poetry](https://github.com/sdispater/poetry) (Python package manager)

Example:

```shell
podman run -p 5432:5432 --name bomber-db -e POSTGRES_PASSWORD=foobar -d postgres
git clone https://github.com/nu11h3r035/back-end-take-home
cd back-end-take-home
poetry install
poetry run alembic upgrade head
poetry run python import_data.py data/full/airlines.csv airline
poetry run python import_data.py data/full/airports.csv airport
poetry run python import_data.py data/full/routes.csv route
poetry run python run.py
```

Note that you can substitute `podman` with `docker` since the APIs match.
