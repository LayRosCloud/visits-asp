using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearnAsp.Middlewares
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey(JwtConstants.TokenType))
            {
                var token = context.Request.Cookies[JwtConstants.TokenType];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                    var jsonToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    var payload = jsonToken.Claims
                        .Select(claim => new { claim.Type, claim.Value })
                        .ToDictionary(c => c.Type, c => c.Value);
                    var id = payload[JwtRegisteredClaimNames.Jti];
                    context.Request.Headers.Add("uid-user", id);
                }
            }

            await _next(context);
        }
    }
}
