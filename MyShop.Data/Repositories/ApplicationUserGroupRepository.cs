using MyShop.Data.Infrastructure;
using MyShop.Data.Models;

namespace MyShop.Data.Respositories
{
    public interface IApplicationUserGroupRepository:IRepository<ApplicationUserGroup>
    {

    }
    public class ApplicationUserGroupRepository : RepositoryBase<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        public ApplicationUserGroupRepository(IDbFactory dbFactory): base(dbFactory)
        {

        }
    }
}
