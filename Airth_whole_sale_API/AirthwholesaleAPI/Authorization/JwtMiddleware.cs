using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.ILogic;
using Microsoft.Extensions.Options;

namespace AirthwholesaleAPI.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettingsDTO _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettingsDTO> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserLogic userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userId);
            }

            await _next(context);
        }
    }
}
