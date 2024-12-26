using AutoMapper;
using EMS_API.Dtos;
using EMS_API.Dtos.Request;
using EMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetProfileById(Guid id)
        {
            var profile = await Task.Run(() => _profileService.GetProfileById(id));
            if (profile == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileDto = _mapper.Map<ProfileDto>(profile);
            return Ok(profileDto);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<ProfileDto>> GetProfileByEmail(string email)
        {
            var profile = await Task.Run(() => _profileService.GetProfileByEmail(email));
            if (profile == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var profileDto = _mapper.Map<ProfileDto>(profile);
            return Ok(profileDto);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ProfileDto>> GetProfileByName(string name)
        {
            var profile = await Task.Run(() => _profileService.GetProfileByName(name));

            if (profile == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileDto = _mapper.Map<ProfileDto>(profile);

            return Ok(profileDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetProfiles()
        {
            var profiles = await Task.Run(() => _mapper.Map<IEnumerable<ProfileDto>>(_profileService.GetProfiles()));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(profiles);
        }

        [HttpPost]
        public async Task<ActionResult<ProfileDto>> CreateProfile([FromBody] ProfileDto newProfile)
        {
            if (newProfile == null)
            {
                return BadRequest(ModelState);
            }

            var profile = _mapper.Map<Models.Profile>(newProfile);

            await Task.Run(() => _profileService.Insert(profile));
            return CreatedAtAction("GetProfiles", new { id = profile.Id }, newProfile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] ProfileDto updateProfile)
        {
            if (updateProfile == null)
            {
                return BadRequest(ModelState);
            }
            var profile = _mapper.Map<Models.Profile>(updateProfile);
            profile.Id = id;
            try
            {
                await Task.Run(() => _profileService.Update(profile));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var profile = await Task.Run(() => _profileService.GetProfileById(id));
            if (profile == null)
            {
                return NotFound();
            }
            await Task.Run(() => _profileService.Delete(id));
            return NoContent();
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordRequest changePassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await Task.Run(() => _profileService.ChangePassword(changePassword.Token, changePassword.OldPassword, changePassword.NewPassword));
            return Ok(result);
        }

        private bool ProfileExists(Guid id)
        {
            return _profileService.GetProfileById(id) != null;
        }
    }
}
