const ERROR_CODES = {
    BadDigest: 400,
    BadMethod: 405,
    ConnectTimeout: 408,
    InternalServerError: 500,
    InvalidArgument: 409,
    ResourceNotFoundError: 404,
    BadRequest: 400,
    MissingParameter: 409,
    EntityNotFound: 404
}

const APP_DATA = {
    AIRLINE: "AIRLINE",
    AIRPORT: "AIRPORT",
    ROUTE: "ROUTE"
}

module.exports = Object.assign({}, { ERROR_CODES, APP_DATA })