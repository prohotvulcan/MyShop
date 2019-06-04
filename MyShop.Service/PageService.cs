using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using MyShop.Data.Repositories;

namespace MyShop.Service
{
    public interface IPageService
    {
        Page GetByAlias(string alias);
    }
    public class PageService : IPageService
    {
        private IUnitOfWork _unitOfWork;
        private IPageRepository _pageRepository;

        public PageService(IUnitOfWork unitOfWork, IPageRepository pageRepository)
        {
            this._unitOfWork = unitOfWork;
            this._pageRepository = pageRepository;
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }
    }
}
