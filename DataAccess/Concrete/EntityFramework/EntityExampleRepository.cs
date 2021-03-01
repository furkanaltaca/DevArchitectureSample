
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class EntityExampleRepository : EfEntityRepositoryBase<EntityExample, ProjectDbContext>, IEntityExampleRepository
    {
        public EntityExampleRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
