﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskMgmt.Services;

namespace TaskMgmt.Api.Attributes
{
    public class GroupMembershipAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private IUserService _userService;
        private readonly string _groupIdParameterName;

        public GroupMembershipAuthorizeAttribute(string groupIdParameterName)
        {
            _groupIdParameterName = groupIdParameterName;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_userService == null)
            {
                _userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            }
            if (!int.TryParse(context.HttpContext.Request.RouteValues[_groupIdParameterName].ToString(), out int groupId))
            {
                context.Result = new BadRequestObjectResult("Invalid group id");
                return;
            }

            if (!int.TryParse(context.HttpContext.User.FindFirst("UserId").Value, out int userId))
            {
                context.Result = new BadRequestObjectResult("Invalid user id");
                return;
            }
            bool isUserInGroup = await _userService.IsUserInGroup(userId, groupId);
            if (!isUserInGroup)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
