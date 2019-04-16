using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cet37Market.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cet37Market.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;

        public UserHelper(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }
    }
}
