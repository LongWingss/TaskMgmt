using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskMgmt.Services;

namespace TaskMgmt.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class GroupMembershipAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {

        private readonly string _groupIdParameterName;

        public GroupMembershipAuthorizeAttribute(string groupIdParameterName)
        {
            _groupIdParameterName = groupIdParameterName;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            IUserService _userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

            if (!int.TryParse(context.HttpContext.Request.RouteValues[_groupIdParameterName]?.ToString(), out int groupId))
            {
                context.Result = new BadRequestObjectResult("Invalid group id");
                return;
            }

            if (!int.TryParse(context.HttpContext.User.FindFirst("UserId")?.Value, out int userId))
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
