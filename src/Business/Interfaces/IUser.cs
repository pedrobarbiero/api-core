using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Business.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid UserID { get; }
        string UserEmail { get; }
        bool IsAuthenticated { get; }
        IEnumerable<Claim> Claims { get; }
        bool IsInRole(string role);
    }
}
