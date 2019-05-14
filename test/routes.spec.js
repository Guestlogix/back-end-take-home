import regeneratorRuntime from "regenerator-runtime";
import { use, request, expect } from 'chai';
import http from 'chai-http';

use(http);

describe('Routes GET testing', () => {

    it('GET all routes', () => {
        request('http://localhost:3000')
            .get('/routes')
            .end((err, res) => {
                expect(err).to.be.null;
                expect(res).to.have.status(200);
                expect(res.body).to.be.an("array");
            });
    });

    it('GET shortest path', () => {
        let origin = "YYZ";
        let destination = "YVR";   

        request('http://localhost:3000')
            .get('/routes/'+ origin +'&' + destination)
            .end((err, res) => {
                expect(err).to.be.null;
                expect(res).to.have.status(200);
                expect(res.body).to.be.an("array");
            });
    });
})