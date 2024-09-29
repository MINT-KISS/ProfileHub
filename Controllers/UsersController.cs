using Microsoft.AspNetCore.Mvc;
using ProfileHub.Interfaces;
using ProfileHub.Models;


namespace ProfileHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IS3Service _s3Service;

        public UsersController(IUserRepository userRepository, IS3Service s3Service)
        {
            _userRepository = userRepository;
            _s3Service = s3Service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => Ok(await _userRepository.GetUsersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _userRepository.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile file)
        {
            var user = await _userRepository.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var keyName = $"users/{id}/{file.FileName}";
            var photoUrl = await _s3Service.UploadFileAsync(file, keyName);

            user.ProfilePhotoUrl = photoUrl;
            await _userRepository.UpdateUserAsync(user);

            return Ok(new { photoUrl });
        }
    }
}