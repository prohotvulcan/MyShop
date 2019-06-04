﻿using MyShop.Common.Exceptions;
using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using MyShop.Data.Respositories;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Service
{
    public interface IApplicationRoleService
    {
        ApplicationRole GetDetail(string id);
        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationRole> GetAll();
        ApplicationRole Add(ApplicationRole appRole);
        void Update(ApplicationRole AppRole);
        void Delete(string id);
        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId);
        //Get list role by group id
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);
        void Save();
    }
    public class ApplicationRoleService : IApplicationRoleService
    {
        private IUnitOfWork _unitOfWork;
        private IApplicationRoleRepository _appRoleRepository;
        private IApplicationRoleGroupRepository _appRoleGroupRepository;

        public ApplicationRoleService(IUnitOfWork unitOfWork,
            IApplicationRoleRepository appRoleRepository,
            IApplicationRoleGroupRepository appRoleGroupRepository)
        {
            this._unitOfWork = unitOfWork;
            this._appRoleRepository = appRoleRepository;
            this._appRoleGroupRepository = appRoleGroupRepository;
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == appRole.Description))
            {
                throw new NameDuplicatedException("Tên không được trùng");
            }
            return _appRoleRepository.Add(appRole);
        }

        public bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId)
        {
            _appRoleGroupRepository.DeleteMulti(x => x.GroupId == groupId);
            foreach (var roleGroup in roleGroups)
            {
                _appRoleGroupRepository.Add(roleGroup);
            }
            return true;
        }

        public void Delete(string id)
        {
            _appRoleRepository.DeleteMulti(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _appRoleRepository.GetAll();
        }

        public ApplicationRole GetDetail(string id)
        {
            return _appRoleRepository.GetSingleByCondition(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            return _appRoleRepository.GetListRoleByGroupId(groupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationRole AppRole)
        {
            if (_appRoleRepository.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _appRoleRepository.Update(AppRole);
        }
    }
}
