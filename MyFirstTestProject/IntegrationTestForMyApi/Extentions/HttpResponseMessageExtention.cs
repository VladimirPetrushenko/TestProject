using FluentAssertions;
using System.Net;
using System.Net.Http;

namespace IntegrationTestForMyApi.Extentions
{
    public static class HttpResponseMessageExtention
    {
        public static void CheckResponse(this HttpResponseMessage response, HttpStatusCode code) =>
            response.StatusCode.Should().Be(code);
    }
}
