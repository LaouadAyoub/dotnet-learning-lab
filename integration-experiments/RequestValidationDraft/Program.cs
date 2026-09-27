internal partial class Program
{

    private static void Main(string[] args)
    {
        List<ApiRequest> requests = new List<ApiRequest>
        {
            new ApiRequest
            {
                RequestId = "REQ-001",
                UserEmail = "rick@example.com",
                PayloadSize = 250
            },
            new ApiRequest
            {
                RequestId = "REQ-002",
                UserEmail = "",
                PayloadSize = 100
            },
            new ApiRequest
            {
                RequestId = "REQ-001",
                UserEmail = "rick@example.com",
                PayloadSize = 250
            },
            new ApiRequest
            {
                RequestId = "REQ-003",
                UserEmail = "  MORTY@EXAMPLE.COM ",
                PayloadSize = 1200
            }
        };



    }

    //static IEnumerable<ApiRequest> FilterRequests(
    //IEnumerable<ApiRequest> requests,
    //Func<ApiRequest, bool> validationRule)
    //{

    //}
}
