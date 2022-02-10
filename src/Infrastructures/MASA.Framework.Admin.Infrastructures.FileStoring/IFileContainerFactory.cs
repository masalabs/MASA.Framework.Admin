using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public interface IFileContainerFactory
    {
        /// <summary>
        /// Gets a named container.
        /// </summary>
        /// <param name="name">The name of the container</param>
        /// <returns>
        /// The container object.
        /// </returns>
        IFileContainer Create(
            string name,
            FileContainerConfiguration configuration
        );
    }
}
