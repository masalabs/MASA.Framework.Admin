using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Sdks.Authentication.Request.Users
{
    public class CreateUserGroupRequest
    {
        [Required]

        public string Name { get; set; } = "";

        [Required]
        public string Code { get; set; } = "";

        public string Describtion { get; set; } = "";
    }
}
