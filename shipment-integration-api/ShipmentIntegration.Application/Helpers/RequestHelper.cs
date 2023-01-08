using RestSharp;

namespace ShipmentIntegration.Application.Helpers;

public static class RequestHelper
{
    public static IRestResponse SendRequestHelper(Method method, string url, string body = null, string token = null)
    {
        var client = new RestClient(url);
        var request = new RestRequest(method);

        request.AddHeader("Accept", "application/json");
        request.AddHeader("content-type", "application/json");

        if (!string.IsNullOrEmpty(token))
            request.AddHeader("Token", token);
        if (!string.IsNullOrEmpty(body))
            request.AddParameter("application/json", body, ParameterType.RequestBody);

        IRestResponse response = client.Execute(request);
        return response;
    }
}
