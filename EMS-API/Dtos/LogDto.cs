namespace EMS_API.Dtos
{
    public class LogDto
    {
        public Guid LogId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid DeviceId { get; set; }
    }
}
