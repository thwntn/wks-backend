namespace Wks.Mains;

public class HttpObject
{
    public class Response<TData>(
        bool isComplete,
        TData data,
        string error,
        int statusCode,
        decimal responseTime
    )
    {
        public bool IsSuccessStatusCode { get; set; } = isComplete;
        public TData Data { get; set; } = data;
        public string Error { get; set; } = error;
        public int StatusCode { get; set; } = statusCode;
        public decimal ResponseTime { get; set; } = responseTime;
    }

    public class Duration(HttpResponseMessage response, decimal responseTime)
    {
        public HttpResponseMessage Response { get; set; } = response;
        public decimal ResponseTime { get; set; } = responseTime;
    }
}
