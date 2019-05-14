import regeneratorRuntime from "regenerator-runtime";
import { use, request, expect } from 'chai';
import http from 'chai-http';

use(http);

describe('Airports GET testing', () => {

    it('GET all airports', () => {
        request('http://localhost:3000')
            .get('/airports')
            .end((err, res) => {
                expect(err).to.be.null;
                expect(res).to.have.status(200);
                expect(res.body).to.be.an("array");
            });
    });

})