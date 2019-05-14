import regeneratorRuntime from "regenerator-runtime";
import { use, request, expect } from 'chai';
import http from 'chai-http';

use(http);

describe('Airlines GET testing', () => {

    it('GET all airlines', () => {
        request('http://localhost:3000')
            .get('/airlines')
            .end((err, res) => {
                expect(err).to.be.null;
                expect(res).to.have.status(200);
                expect(res.body).to.be.an("array");
            });
    });

})