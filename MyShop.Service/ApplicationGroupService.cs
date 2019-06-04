using MyShop.Common.Exceptions;
using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using MyShop.Data.Respositories;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);
        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationGroup> GetAll();
        ApplicationGroup Add(ApplicationGroup appGroup);
        void Update(ApplicationGroup appGroup);
        ApplicationGroup Delete(int id);
        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> groups, string userId);
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
        void Save();
    }

    public class ApplicationGroupService : IApplicationGroupService
    {
        private IUnitOfWork _unitOfWork;
        private IApplicationGroupRepository _appGroupRepository;
        private IApplicationUserGroupRepository _appUserGroupRepository;

        public ApplicationGroupService(IUnitOfWork unitOfWork,
            IApplicationGroupRepository appGroupRepository,
            IApplicationUserGroupRepository appUserGroupRepository)
        {
            this._unitOfWork = unitOfWork;
            this._appGroupRepository = appGroupRepository;
            this._appUserGroupRepository = appUserGroupRepository;
        }

        public ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appGroupRepository.Add(appGroup);
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> groups, string userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in groups)
            {
                _appUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public ApplicationGroup Delete(int id)
        {
            var appGroup = this._appGroupRepository.GetSingleById(id);
            return _appGroupRepository.Delete(appGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }
            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _appGroupRepository.GetAll();
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _appGroupRepository.GetSingleById(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name && x.ID != appGroup.ID))
            {
                throw new NameDuplicatedException("Tên không được trùng");
            }
            _appGroupRepository.Update(appGroup);
        }
    }
}
