using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Infrastructure.Common.DB;

namespace TestNetty.Data
{
    public class DemoContext : DbContext, IContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=123456;database=mycompany;Character Set=utf8;Convert Zero Datetime=True;TreatTinyAsBoolean=false;");
        }

        public virtual DbSet<Employee> Employee { get; set; }
    }
}
