namespace EMS_API.Dtos.Request
{
    public class ResetPasswordRequest
    {
        public string Username { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
