using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.DB
{
    public interface IEFRepositoryFactory
    {
        IEFRepository<T> CreateRepository<T>(IContext mydbcontext) where T : class;
    }
}
