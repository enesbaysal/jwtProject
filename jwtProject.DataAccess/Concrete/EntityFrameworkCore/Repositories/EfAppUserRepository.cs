using jwtProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using jwtProject.DataAccess.Interfaces;
using jwtProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jwtProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUser
    {
        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            //efcore 5.0 include(I=>I.AppRole.where(a=>a.roleName==name))

            using var context = new JwtContext();
            return await context.AppUsers.Join(context.AppUserRoles, u => u.Id, ur => ur.AppUserId, (user, userRole) => new
            {
                user = user,
                userRole = userRole
            }).Join(context.AppRoles, two => two.userRole.AppRoleId, r => r.Id, (twoTable, role) => new
            {
                user = twoTable.user,
                userRole = twoTable.userRole,
                role = role
            }).Where(I => I.user.UserName == userName).Select(I => new AppRole
            {
                Id = I.role.Id,
                Name = I.role.Name
            }).ToListAsync();
        }
    }
}
