using System.Security.Cryptography;
using System.Text;

namespace MASA.Framework.Admin.Service.Api.Infrastructure.Utils
{
    public class SHA1Utils
    {
        public static string Encrypt(string content, bool isRemoveConnector = true, bool isToLower = false)
        {
            SHA1 sHA = SHA1.Create();
            byte[] value = sHA.ComputeHash(Encoding.UTF8.GetBytes(content));
            string text = BitConverter.ToString(value);
            if (isRemoveConnector)
            {
                text = text.Replace("-", "");
            }

            if (isToLower)
            {
                text = text.ToLower();
            }

            return text;
        }
    }
}
