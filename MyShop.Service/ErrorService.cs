using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using MyShop.Data.Respositories;

namespace MyShop.Service
{
    public interface IErrorService
    {
        Error Create(Error error);
        void Save();
    }
    public class ErrorService : IErrorService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorRepository _errorRepository;

        public ErrorService(IUnitOfWork unitOfWork, IErrorRepository errorRepository)
        {
            this._unitOfWork = unitOfWork;
            this._errorRepository = errorRepository;
        }

        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
