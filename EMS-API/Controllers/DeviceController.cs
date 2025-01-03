﻿using AutoMapper;
using EMS_API.Dtos;
using EMS_API.Models;
using EMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(IDeviceService deviceService, IMapper mapper)
        {
            _deviceService = deviceService;
            _mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDevices()
        {
            var devices = await Task.Run(() => _mapper.Map<IEnumerable<DeviceDto>>(_deviceService.GetDevices()));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(devices);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> GetDeviceById(Guid id)
        {
            var device = await Task.Run(() => _mapper.Map<DeviceDto>(_deviceService.GetDeviceById(id)));

            if (device == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(device);
        }


        [HttpGet("mac/{mac}")]
        public async Task<ActionResult<DeviceDto>> GetDeviceByMac(string mac)
        {
            var device = await Task.Run(() => _mapper.Map<DeviceDto>(_deviceService.GetDeviceByMAC(mac)));

            if (device == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(device);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(Guid id, [FromBody] DeviceDto updateDevice)
        {
            if (updateDevice == null)
            {
                return BadRequest(ModelState);
            }

            var device = _mapper.Map<Device>(updateDevice);
            device.Id = id;

            try
            {
                await Task.Run(() => _deviceService.Update(device));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<DeviceDto>> CreateDevice([FromBody] DeviceDto newDevice)
        {
            if (newDevice == null)
            {
                return BadRequest(ModelState);
            }

            var device = _mapper.Map<Device>(newDevice);

            await Task.Run(() => _deviceService.Insert(device));
            return CreatedAtAction("GetDevices", new { id = device.Id }, newDevice);
        }

        // DELETE: api/Device/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            var device = await Task.Run(() => _mapper.Map<DeviceDto>(_deviceService.GetDeviceById(id)));
            if (device == null)
            {
                return NotFound();
            }

            await Task.Run(() => _deviceService.Delete(id));
            return NoContent();
        }

        private bool DeviceExists(Guid id)
        {
            return _deviceService.GetDeviceById(id) != null;
        }
    }
}
