using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Extensions;
using Shop.API.Repositories.Contracts;
using Shop.Models.Dtos;

namespace Shop.API.Controllers
{
    /// <summary>
    ///     Controller for handling product-related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        ///     Returns all users in the Users table.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            // Try to execute the following code
            try
            {
                // Fetch all users from the repository
                var users = await _userRepository.GetUsers();

                // If the users are null, return a NotFound status
                if (users == null)
                {
                    return NotFound();
                }
                // Convert the users to DTOs
                var userDtos = users.ConvertToDto();

                // Return the converted DTOs with an Ok status
                return Ok(userDtos);
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error status with a custom error message
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
    }
}
