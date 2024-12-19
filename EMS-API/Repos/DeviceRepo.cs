using EMS_API.Data;
using EMS_API.Models;

namespace EMS_API.Repos
{
    public class DeviceRepo
    {
        private readonly EMSDbContext _context;
        public DeviceRepo(EMSDbContext context)
        {
            _context = context;
        }

        public bool Insert(Device device)
        {
            _context.Devices.Add(device);
            return Save();
        }

        public bool Update(Device device)
        {
            _context.Devices.Update(device);
            return Save();
        }

        public bool Delete(Device device)
        {
            _context.Devices.Remove(device);
            return Save();
        }

        public Device? GetDeviceById(Guid id)
        {
            return _context.Devices.Find(id);
        }

        public Device? GetDeviceBySerialNumber(string serialNumber)
        {
            return _context.Devices.FirstOrDefault(d => d.SerialNumber == serialNumber);
        }

        public Device? GetDeviceByMAC(string mac)
        {
            return _context.Devices.FirstOrDefault(d => d.MAC == mac);
        }

        public List<Device> GetDevices()
        {
            return _context.Devices.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
