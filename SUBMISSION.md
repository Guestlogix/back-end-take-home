# Submission
## Prerequisites
This solution was tested with the following already installed on the machine:
- NodeJS v10.16.0
- npm v6.9.0
- TypeScript v3.5.2
  - npm install -g typescript
- Windows 10

## Install
Install all dependencies first before attempting to run the server.

	$ npm install

## Running the Server

	$ npm start

The site is hosted at port 3030 by default. 

If you need it running in a different port, such as 1234...

	$ npm start -- --port=1234

Site index can be accessed at: http://localhost:3030

Route endpoint can be accessed at: http://localhost:3030/route?origin=&destination=

## Testing

	$ npm test