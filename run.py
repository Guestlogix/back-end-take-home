import os

from bomber.app import create_app

app = create_app()


if __name__ == "__main__":
    app.run(
        host=os.getenv("BOMBER_LISTEN_ADDRESS", "127.0.0.1"),
        port=os.getenv("BOMBER_LISTEN_PORT", 9000),
    )
