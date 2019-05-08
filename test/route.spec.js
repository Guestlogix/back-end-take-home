const app = require("../server.js")
const request = require("supertest")(app);
const expect = require('expect.js');

describe('Flight Route', () => {

  it('should return shortest flight route', function(done) {
    const source = 'ABJ';
    const destination = 'OUA';
    request
      .get(`/api/routes?origin=${source}&destination=${destination}`)
      .set('Content-Type', 'application/json')
      .expect(200)
      .end(function(err, res) {
        if(err) {
          return err;
        }
        expect(res.status).to.be(200)
        expect(res.body.message).to.be('Found shortest route')
        expect(res.body.airports.length).to.not.be(0)
        done();
      });
  });

  it('should return Source or destination not found when invalid origin and destination is searched', function(done) {
    const source = 'CCC';
    const destination = 'JUX';
    request
      .get(`/api/routes?origin=${source}&destination=${destination}`)
      .set('Content-Type', 'application/json')
      .expect(400)
      .end(function(err, res) {
        if(err) {
          return err;
        }
        expect(res.status).to.be(400)
        expect(res.body.message).to.be('Source or destination not found')
        done();
      });
  });

});
