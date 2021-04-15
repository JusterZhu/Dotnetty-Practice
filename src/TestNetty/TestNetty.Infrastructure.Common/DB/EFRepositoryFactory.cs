using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.DB
{
    public class EFRepositoryFactory : IEFRepositoryFactory
    {
        public IEFRepository<T> CreateRepository<T>(IContext context) where T : class
        {
            return new EFRepository<T>(context as DbContext);
        }
    }
}
