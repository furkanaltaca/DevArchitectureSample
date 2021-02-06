using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete.EntityFramework
{
    public class MobileLoginRepository : EfEntityRepositoryBase<MobileLogin, ProjectDbContext>, IMobileLoginRepository
    {
        public MobileLoginRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
