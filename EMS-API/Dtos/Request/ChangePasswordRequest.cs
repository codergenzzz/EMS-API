namespace EMS_API.Dtos.Request
{
    public class ChangePasswordRequest
    {
        public string Token { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
