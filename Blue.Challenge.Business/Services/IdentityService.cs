using Blue.Challenge.Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Blue.Challenge.Business.Services
{
    public class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            return new Guid(userId);

        }
    }
}
