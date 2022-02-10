﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileProviderDeleteParameters : FileProviderParameters
    {
        public FileProviderDeleteParameters(
            string containerName,
            FileContainerConfiguration configuration,
            string blobName,
            CancellationToken cancellationToken = default)
            : base(
                  containerName, 
                  configuration, 
                  blobName, 
                  cancellationToken)
        {
        }
    }
}
