using System.Text.Json.Serialization;

namespace MesMachineSim.Models
{
    public class LoginReqDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}