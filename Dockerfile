FROM python:3.6-alpine

RUN apk update && \
    apk add curl

COPY requirements.txt .
RUN pip install -r requirements.txt

# COPY ./entrypoint.sh /
# ENTRYPOINT ["sh", "/entrypoint.sh"]

COPY . /flight_router
WORKDIR /flight_router

EXPOSE 5000

# CMD ["python", "manager.py", "runserver"]
