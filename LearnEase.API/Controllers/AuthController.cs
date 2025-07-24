using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.Models.Request;
using Google.Apis.Auth;
using LearnEase.Repository;
using LearnEase.Service.Models.Response;
using LearnEase.Service.IServices;

namespace LearnEase.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LearnEaseContext _context;
        private readonly IConfiguration _config;
		private readonly IEmailService _emailService;

		public AuthController(LearnEaseContext context, IConfiguration config, IEmailService emailService)
        {
            _context = context;
            _config = config;
			_emailService = emailService;
		}

		[HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                return BadRequest("Username already exists.");

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.UserHearts.Add(new UserHeart
            {
                UserId = user.UserId,
                CurrentHearts = 5,
                LastUsedAt = null,
                LastRegeneratedAt = null
            });
            await _context.SaveChangesAsync();

            return Ok("Registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == request.Username && u.Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            var token = new JwtHelper(_config).GenerateToken(user);
            var userResponse = new UserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };

            return Ok(new LoginResponse
            {
                Token = token,
                User = userResponse
            });
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            Console.WriteLine("⏱️ GoogleLogin bắt đầu");

            if (string.IsNullOrWhiteSpace(request.IdToken))
                return BadRequest("IdToken is required.");

            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
                Console.WriteLine("✅ Token OK");

                var name = payload.Name;
                if (string.IsNullOrEmpty(payload.Email))
                    return BadRequest("Email not found in token.");

                var user = _context.Users.FirstOrDefault(u => u.Email == payload.Email);

                if (user == null)
                {
                    Console.WriteLine("🆕 Chưa có user → tạo mới");

                    user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Username = name,
                        Email = payload.Email,
                        Password = null,
                        AvatarUrl = payload.Picture
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    _context.UserHearts.Add(new UserHeart
                    {
                        UserId = user.UserId,
                        CurrentHearts = 5,
                        LastUsedAt = null,
                        LastRegeneratedAt = null
                    });
                    await _context.SaveChangesAsync();

                    Console.WriteLine("✅ Tạo user mới thành công");
                }

                Console.WriteLine("🎫 Tạo token...");
                var token = new JwtHelper(_config).GenerateToken(user);

                var userResponse = new UserResponse
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    AvatarUrl = user.AvatarUrl
                };

                Console.WriteLine("🚀 Trả về response thành công");
                return Ok(new LoginResponse
                {
                    Token = token,
                    User = userResponse
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi xử lý Google login: " + ex.Message);
                return StatusCode(500, "Google login error: " + ex.Message);
            }
        }

        /*  public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
          {
              if (string.IsNullOrWhiteSpace(request.IdToken))
                  return BadRequest("IdToken is required.");

              try
              {
                  // Validate token
                  var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
                  if (payload == null)
                      return Unauthorized("Invalid Google token.");


                  var name = payload.Name;

                  if (string.IsNullOrEmpty( payload.Email))
                      return BadRequest("Email not found in token.");

                  // Check if user exists
                  var user = _context.Users.FirstOrDefault(u => u.Email == payload.Email);

                  if (user == null)
                  {
                      // Create new user
                      user = new User
                      {
                          UserId = Guid.NewGuid(),
                          Username = name,
                          Email = payload.Email,
                          Password = null,
                          AvatarUrl= payload.Picture,
                      };

                      _context.Users.Add(user);
                      await _context.SaveChangesAsync();
                      // send mail to user
                      try
                      {
                          await _emailService.SendWelcomeEmailAsync(user.Email, user.Username);
                      }
                      catch (Exception ex)
                      {
                          // Ghi log nhưng không throw để tránh làm fail login
                          Console.WriteLine("⚠️ SendWelcomeEmail failed: " + ex.Message);
                      }

                  }

                  // Generate JWT token
                  var token = new JwtHelper(_config).GenerateToken(user);

                  var userResponse = new UserResponse
                  {
                      UserId = user.UserId,
                      Username = user.Username,
                      Email = user.Email,
                      AvatarUrl = user.AvatarUrl
                  };

                  return Ok(new LoginResponse
                  {
                      Token = token,
                      User = userResponse
                  });

              }
              catch (InvalidJwtException ex)
              {
                  return Unauthorized($"Invalid Google token: {ex.Message}");
              }
              catch (Exception ex)
              {
                  return StatusCode(500, $"Internal server error: {ex.Message}");
              }
          }*/
    }
}
