using MyShop.Data.Infrastructure;
using MyShop.Data.Models;

namespace MyShop.Data.Respositories
{
    public interface IErrorRepository: IRepository<Error>
    {

    }
    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
