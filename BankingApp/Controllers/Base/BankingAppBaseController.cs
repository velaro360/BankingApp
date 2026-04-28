using Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingApp.Controllers.Base
{
    [ApiController]
    public abstract class BankingAppBaseController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("User id claim is missing.");

            return int.Parse(userId);
        }
    }
}
