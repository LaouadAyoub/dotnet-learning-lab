using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        // Create one new normal request
        ApiRequest apiRequest = new ApiRequest
        {
            RequestId = "0",
            UserEmail = "test@example.com",
            PayloadSize = 32,
        };
        // Create one with empty email
        ApiRequest apiRequestEmptyEmail = new ApiRequest
        {
            RequestId = "1",
            UserEmail = "",
            PayloadSize = 35,
        };

        // Create one request with Payload too large
        ApiRequest apiRequestLargePayload = new ApiRequest
        {
            RequestId = "2",
            UserEmail = "large@example.com",
            PayloadSize = 2000,
        };

        Console.WriteLine("Hello, World!");

        // create a list of requests
        List<ApiRequest> apiRequests = new List<ApiRequest>
        {
            apiRequest,
            apiRequestEmptyEmail,
            apiRequestLargePayload
        };
        // Remove empty emails
        IEnumerable<ApiRequest> filteredrequests = FilterRequests(apiRequests, request => !string.IsNullOrWhiteSpace(request.UserEmail));

        // Remove Oversized payloads
        filteredrequests = FilterRequests(filteredrequests, request => request.PayloadSize < 1000);


        // Remove duplicates
        List<ApiRequest> uniqueRequests = new List<ApiRequest>();
        HashSet<string> processedRequestIds = new HashSet<string>();

        foreach (var request in filteredrequests)
        {
            if (processedRequestIds.Add(request.RequestId))
            {
                uniqueRequests.Add(request);
            }
        }

        // Order by Payloadsize
        IEnumerable<ApiRequest> sortedRequests = uniqueRequests.OrderBy(request => request.PayloadSize);

        // Display requests
        foreach (var request in sortedRequests)
        {
            Console.WriteLine($"RequestId : {request.RequestId} \n UserEmail : {request.UserEmail} \n PayloadSize : {request.PayloadSize} \n");
        }

        // Project requests into a string
        IEnumerable<string> requestSummaries = sortedRequests.Select(request => $"{request.RequestId} | {request.UserEmail} | {request.PayloadSize}\n");

        foreach (var requestSummary in requestSummaries)
        {
            Console.WriteLine($"{requestSummary}");
        }

        ProcessRequests(sortedRequests, DisplayRequest);

        Console.WriteLine("Done");
    }

    static void DisplayRequest(ApiRequest request)
    {
        Console.WriteLine($"DisplayRequest : RequestId : {request.RequestId} \n UserEmail : {request.UserEmail} \n PayloadSize : {request.PayloadSize} \n");
    }

    static void ProcessRequests(IEnumerable<ApiRequest> requests, Action<ApiRequest> ProcessingAction)
    {
        foreach (var request in requests)
        {
            ProcessingAction(request);
        }
    }

    static IEnumerable<ApiRequest> FilterRequests(
        IEnumerable<ApiRequest> requests,
        Func<ApiRequest, bool> validationRule)
    {
        List<ApiRequest> filteredList = new List<ApiRequest>();

        foreach (ApiRequest apiRequest in requests)
        {
            if (validationRule(apiRequest))
            {
                filteredList.Add(apiRequest);
            }
        }
        return filteredList;
    }

    public class ApiRequest
    {
        public string RequestId { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        public int PayloadSize { get; set; }
    }


}
