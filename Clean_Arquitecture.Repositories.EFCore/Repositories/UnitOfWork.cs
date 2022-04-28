using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Repositories.EFCore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly Clean_ArquitectureContext Context;
        public UnitOfWork(Clean_ArquitectureContext context) =>
            Context = context;
        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
