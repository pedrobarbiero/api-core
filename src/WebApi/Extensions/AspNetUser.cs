using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebApi.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _acessor;

        public AspNetUser(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public string Name => _acessor.HttpContext.User.Identity.Name;

        public Guid UserID => IsAuthenticated ? Guid.Parse(_acessor.HttpContext.User.UserId()) : Guid.Empty;

        public string UserEmail => IsAuthenticated ? _acessor.HttpContext.User.UserEmail() : string.Empty;

        public bool IsAuthenticated => _acessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> Claims => _acessor.HttpContext.User.Claims;

        public bool IsInRole(string role) => _acessor.HttpContext.User.IsInRole(role);

    }

    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string UserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
