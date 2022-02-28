using System.ComponentModel.DataAnnotations;

namespace Masa.Framework.Sdks.Authentication.Request.Users
{
    public class CreateGroupRequest
    {
        [Required]

        public string Name { get; set; } = "";

        [Required]
        public string Code { get; set; } = "";

        public string Describtion { get; set; } = "";
    }
}
