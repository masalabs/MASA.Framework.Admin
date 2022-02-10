using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Order.Model
{
    public class AuthOptions
    {
        /// <summary>
        /// Jwt认证Key
        /// </summary>
        public string Security { get; set; } = default!;

        /// <summary>
        /// 过期时间【天】
        /// </summary>
        public int Expiration { get; set; }
    }
}
