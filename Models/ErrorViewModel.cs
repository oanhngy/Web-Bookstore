namespace BookstoreWeb.Models
{
    public class ErrorViewModel
    {
        public string? RequestId {get; set; } //mã request, dùng debug
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}