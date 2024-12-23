using AutoMapper;
using EMS_API.Dtos;
using EMS_API.Models;
using EMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET: api/role
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await Task.Run(() => _mapper.Map<IEnumerable<RoleDto>>(_roleService.GetRoles()));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(roles);
        }

        // GET: api/role/id
        [HttpGet("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Role>> GetRoleById(Guid id)
        {
            var role = await Task.Run(() => _mapper.Map<RoleDto>(_roleService.GetRoleById(id)));

            if (role == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(role);
        }
    }
}
