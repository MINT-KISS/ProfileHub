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

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() => Ok(await _userRepository.GetUsersAsync());

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.GetUserAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>No content.</returns>
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

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Upload a photo for a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="file">The photo file to upload.</param>
        /// <returns>The URL of the uploaded photo.</returns>
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