using EMS_API.Models;
using EMS_API.Repos;

namespace EMS_API.Services.Implements
{
    public class DeviceServiceImpl : IDeviceService
    {
        private readonly DeviceRepo _deviceRepo;
        public DeviceServiceImpl(DeviceRepo deviceRepo)
        {
            _deviceRepo = deviceRepo;
        }
        public void Delete(Guid id)
        {
            var device = _deviceRepo.GetDeviceById(id);
            if (device == null)
            {
                throw new KeyNotFoundException("Device not found.");
            }

            var result = _deviceRepo.Delete(device);
            if (!result)
            {
                throw new Exception("Failed to delete device.");
            }
        }

        public Device GetDeviceById(Guid id)
        {
            var device = _deviceRepo.GetDeviceById(id);
            if (device == null)
            {
                throw new KeyNotFoundException("Device not found.");
            }
            return device;
        }

        public Device GetDeviceBySerialNumber(string serialNumber)
        {
            var device = _deviceRepo.GetDeviceBySerialNumber(serialNumber);
            if (device == null)
            {
                throw new KeyNotFoundException("Device not found.");
            }
            return device;
        }
        public Device GetDeviceByMAC(string mac)
        {
            var device = _deviceRepo.GetDeviceByMAC(mac);
            if (device == null)
            {
                throw new KeyNotFoundException("Device not found.");
            }
            return device;
        }

        public Device Insert(Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            var result = _deviceRepo.Insert(device);
            if (!result)
            {
                throw new Exception("Failed to insert device.");
            }

            return device;
        }

        public Device Update(Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            var existingDevice = _deviceRepo.GetDeviceById(device.DeviceId);

            if (existingDevice == null)
            {
                throw new KeyNotFoundException("Device not found.");
            }

            var result = _deviceRepo.Update(device);
            if (!result)
            {
                throw new Exception("Failed to update device.");
            }

            return device;
        }

        public ICollection<Device> GetDevices()
        {
            return _deviceRepo.GetDevices();
        }

    }
}
