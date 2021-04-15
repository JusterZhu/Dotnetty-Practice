using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.DB
{
    public class EFContext : DbContext, IContext
    {
        public EFContext() { }
    }
}
