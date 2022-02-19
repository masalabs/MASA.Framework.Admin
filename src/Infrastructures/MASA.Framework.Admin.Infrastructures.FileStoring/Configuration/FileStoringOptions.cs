using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileStoringOptions
    {
        public FileContainerConfiguration Container { get; }

        public FileStoringOptions()
        {
            Container = new FileContainerConfiguration();
        }
    }
}
