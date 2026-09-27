internal partial class Program
{
    public class ApiRequest
    {
        public string RequestId { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int PayloadSize { get; set; }
    }
}
