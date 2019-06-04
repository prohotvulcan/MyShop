using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using MyShop.Data.Repositories;

namespace MyShop.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);
        void Save();
    }
    public class FeedbackService : IFeedbackService
    {
        private IUnitOfWork _unitOfWork;
        private IFeedbackRepository _feedbackRepository;

        public FeedbackService(IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepository)
        {
            this._unitOfWork = unitOfWork;
            this._feedbackRepository = feedbackRepository;
        }

        public Feedback Create(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
