namespace EMS_API.Dtos.Response
{
    public class AuthenticationResponse
    {
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }

    }
}
