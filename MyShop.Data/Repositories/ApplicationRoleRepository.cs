﻿using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Data.Respositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);
    }
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            var query = from r in DbContext.ApplicationRoles
                        join rg in DbContext.ApplicationRoleGroups
                        on r.Id equals rg.RoleId
                        where rg.GroupId == groupId
                        select r;
            return query;
        }
    }
}
