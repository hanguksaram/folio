using ERP.Core.Enums;

namespace ERP.Core.Services
{
    using System.Collections.Generic;

    using ERP.Core.Models;

    public interface IUserService: ICrudService<User>
    {
        User Get(string email, string password);

        User Get(string email);

        IEnumerable<User> GetUsers();

        IEnumerable<User> GetAvalibleByDomain(string p);
        IEnumerable<UserRole> GetUserRoles(int userId);
        bool ChangePassword(string email, string oldPassword, string newPassword);
        bool ResetPassword(string email, string newPassword);
        string GetPasswordCode(string password, string salt);
    }
}