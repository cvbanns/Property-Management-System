using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.Enums;
using HotelManagement.Core.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Infrastructure.Context;
using System.Data.Entity;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AdminRepository : GenericRepository<Manager>, IAdminRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly HotelDbContext _hotelDbContext;

        public AdminRepository(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _hotelDbContext = hotelDbContext;
        }

        public async Task<bool> CreateRole(RoleDTO role)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = ((Roles)role.RoleName).ToString(),
            };
            var result = await _roleManager.CreateAsync(identityRole);
            

            return result.Succeeded;
        }

        public async Task<bool> AddUserRole(string userId, Roles role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if(user == null) return false;
            
            var result = await _userManager.AddToRoleAsync(user, role.ToString());
            
            return result.Succeeded;
        }

        public async Task<bool> RemoveUserRole(string userId, Roles role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null) return false;

            var result = await _userManager.RemoveFromRoleAsync(user, role.ToString());
         
            return result.Succeeded;
        }

        public async Task<Manager> GetAllManager(string Id, Roles roles)
        {

            var result = _hotelDbContext.Managers
                            .Include(c => c.AppUser)
                            .Where(c => c.Id == Id)
                            .FirstOrDefault(m => m.Id == Id);
            return result;
        }

        public Task<string> GetAllManagers(Roles roles)
        {
            throw new NotImplementedException();
        }
    }
}
