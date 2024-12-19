using EMS_API.Models;

namespace EMS_API.Services
{
    public interface IDeviceService
    {
        public Device GetDeviceById(Guid id);
        public Device GetDeviceBySerialNumber(string serialNumber);
        public Device GetDeviceByMAC(string mac);
        public ICollection<Device> GetDevices();
        public Device Insert(Device device);
        public Device Update(Device device);
        public void Delete(Guid id);
    }
}
