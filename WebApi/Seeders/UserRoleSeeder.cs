using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Seeders
{
    public class UserRoleSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager; 

        public UserRoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager; 
        }

        public async void SeedRoles()
        {
            //var adminRoleBool = _roleManager.RoleExistsAsync("admin").Result;
            //var clientRoleBool = _roleManager.RoleExistsAsync("client").Result;
            //var healthWorkerRoleBool = _roleManager.RoleExistsAsync("healthworker").Result;

            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await _roleManager.CreateAsync(new IdentityRole("healthworker"));
            await _roleManager.CreateAsync(new IdentityRole("client"));
        }
    }
}
