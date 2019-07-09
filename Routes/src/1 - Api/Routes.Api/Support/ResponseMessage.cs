using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Routes.Api.Support
{
    public class ResponseMessage
    {
        /// <summary>
        ///     <see cref="HttpRequestMessage" />
        /// </summary>
        public HttpRequestMessage Request { get; private set; }

        /// <summary>
        /// Create a new instance of class <see cref="ResponseMessage"/>
        /// </summary>
        /// <param name="request"></param>
        public ResponseMessage(HttpRequestMessage request)
        {
            Request = request;
        }

        /// <summary>
        /// Create a response
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="data"></param>
        /// <param name="jsonMediaTypeFormatter"></param>
        /// <returns></returns>
        public HttpResponseMessage CreateNoAsync(HttpStatusCode httpStatusCode, object data, JsonMediaTypeFormatter jsonMediaTypeFormatter = null)
        {
            HttpResponseMessage response;

            try
            {
                response = jsonMediaTypeFormatter == null ?
                    Request.CreateResponse(httpStatusCode, data) :
                    Request.CreateResponse(httpStatusCode, data, jsonMediaTypeFormatter);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message, jsonMediaTypeFormatter);
            }

            return response;
        }
    }
}