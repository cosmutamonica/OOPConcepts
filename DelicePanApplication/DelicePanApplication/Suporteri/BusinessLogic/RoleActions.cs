using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Suporteri.BusinessLogic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            // Access the application context and create result variables.
            ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "admin" role if it doesn't already exist.
            if (!roleMgr.RoleExists("Admin"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Admin" });
            }
            //// Then, you create the "suporter" role if it doesn't already exist.
            if (!roleMgr.RoleExists("Client"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "Client" });
            }
            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };
            IdUserResult = userMgr.Create(appUser, "Pa$$word1");

            //// If the new "admin" user was successfully created, 
            //// add the "admin" user to the "admn" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail("admin@admin.com").Id, "Admin"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("admin@admin.com").Id, "Admin");
            }

            var appUser2 = new ApplicationUser
            {
                UserName = "client",
                Email = "client@test.com"
            };
            IdUserResult = userMgr.Create(appUser2, "Freed0mlife@28");

            //// If the new "admin" user was successfully created, 
            //// add the "admin" user to the "admn" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail("client@test.com").Id, "Client"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("client@test.com").Id, "Client");
            }
        }
    }
}