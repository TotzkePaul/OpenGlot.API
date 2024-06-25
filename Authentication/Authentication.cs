using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using PolyglotAPI.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace PolyglotAPI.Authentication
{    
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(" ").Last();

                

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token) as JwtSecurityToken;

                // Extract the 'kid' from the JWT header
                var kid = jwtToken.Header["kid"]?.ToString();
                if (string.IsNullOrEmpty(kid))
                {
                    return AuthenticateResult.Fail("Missing 'kid' in token header");
                }
                // Create claims identity and add the 'kid' as a claim
                var claims = jwtToken.Claims.ToList();
                claims.Add(new Claim("kid", kid));


                var identity = new ClaimsIdentity(claims.AsEnumerable(), Scheme.Name);
                var principal = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"An error occurred: {ex.Message}");
            }
        }
    }
}
