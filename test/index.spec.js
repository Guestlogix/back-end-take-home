import regeneratorRuntime from "regenerator-runtime";
import { use, request, expect } from 'chai';
import http from 'chai-http';

use(http);

describe('Index GET testing', () => {

    it('Get initial info', () => {
        request('http://localhost:3000')
            .get('/')
            .end((err, res) => {
                expect(err).to.be.null;
                expect(res).to.have.status(200); ;
            });
    });
})