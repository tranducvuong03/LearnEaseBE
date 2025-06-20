using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEase.Service.Models.Response
{
    public class LoginResponse
    {
		public string Token { get; set; }
		public UserResponse User { get; set; }
	}
	public class UserResponse
	{
		public Guid UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string AvatarUrl { get; set; }
	}
}
