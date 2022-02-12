﻿using MASA.Framework.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Repositories
{
    public class OperationLogRepository : IOperationLogRepository, IDisposable
    {
        private readonly AdminDbContext _dbContext;

        public OperationLogRepository(AdminDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<OperationLog> GetAll()
        {
            return _dbContext.OperationLogs.AsQueryable();
        }

        public bool Create(OperationLog operationLog)
        {
            _dbContext.Add(operationLog);
            var success = _dbContext.SaveChanges();

            return success > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}