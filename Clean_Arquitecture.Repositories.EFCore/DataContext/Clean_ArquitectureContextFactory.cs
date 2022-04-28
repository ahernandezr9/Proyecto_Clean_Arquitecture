using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Repositories.EFCore.DataContext
{
    class Clean_ArquitectureContextFactory :
        IDesignTimeDbContextFactory<Clean_ArquitectureContext>
    {
        public Clean_ArquitectureContext CreateDbContext(string[] args)
        {
            var OptionBuilder =
                new DbContextOptionsBuilder<Clean_ArquitectureContext>();
            OptionBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;database=Clean_ArquitectureDB");
            return new Clean_ArquitectureContext(OptionBuilder.Options);
        }
    }
}