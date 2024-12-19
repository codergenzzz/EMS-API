namespace EMS_API.Dtos
{
    public class ProfileDto
    {
        public Guid ProfileId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateOnly Birthdate { get; set; }
    }
}
