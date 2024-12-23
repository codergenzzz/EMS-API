namespace EMS_API.Dtos.Response
{
    public class AuthenticationResponse
    {
        public string? Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public int ExpiresIn { get; set; }
        public List<string>? Errors { get; set; }

    }
}
