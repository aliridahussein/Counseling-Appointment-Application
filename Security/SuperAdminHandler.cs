﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Security
{
    public class SuperAdminHandler :
    AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
