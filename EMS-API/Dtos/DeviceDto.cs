namespace EMS_API.Dtos
{
    public class DeviceDto
    {
        public Guid DeviceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } // 1-on, 0-off
        public string MAC { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;    // kitchen, living room, bedroom, etc.
    }
}
