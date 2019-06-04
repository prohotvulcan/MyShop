using MyShop.Common;
using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using MyShop.Data.Repositories;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlides();
        SystemConfig GetSystemConfig(string code);
    }

    public class CommonService : ICommonService
    {
        private IFooterRepository _footerRepository;
        private ISystemConfigRepository _systemConfigRepository;
        private IUnitOfWork _unitOfWork;
        private ISlideRepository _slideRepository;

        public CommonService(IUnitOfWork unitOfWork,
            ISystemConfigRepository systemConfigRepository,
            IFooterRepository footerRepository,
            ISlideRepository slideRepository)
        {
            this._unitOfWork = unitOfWork;
            this._footerRepository = footerRepository;
            this._systemConfigRepository = systemConfigRepository;
            this._slideRepository = slideRepository;
        }

        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x => x.Status);
        }

        public SystemConfig GetSystemConfig(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code);
        }
    }
}
