using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Enums;

namespace ERP.Services
{
    using ERP.Core.Models;
    using ERP.Core.Repository;
    using ERP.Core.Services;

    public class UserService : CrudService<User>, IUserService
    {
        public UserService(IRepo<User> repo)
            : base(repo)
        {
        }

        public User Get(string email, string password)
        {
            email = email.ToLower();

            var user = Get(email);

            if (user == null)
                return null;
            
            string passwordCode = GetPasswordCode(password, user.Salt);


            if (passwordCode == user.Password)
            {
                return user;
            }

            return null;
        }

        public User Get(string email)
        {
            return this.Repo.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return this.Repo.Where(x => !x.IsSa).OrderBy(x => x.FirstName);
        }

        public IEnumerable<User> GetAvalibleByDomain(string p)
        {
            if (string.IsNullOrWhiteSpace(p))
            {
                return new List<User>(0);
            }
            var domains = p.Split(new[] {';'}).Select(x => string.Format("@{0}",x.ToLower()));
            return this.Repo.Where(x => domains.Any(e => x.Email.ToLower().EndsWith(e)));
        }

        public IEnumerable<UserRole> GetUserRoles(int userId)
        {
            
            var rez = new List<UserRole>();
            if (userId > 0)
            {
                var test = this.Repo.Get(userId).Companies.ToList();

                var u = test.Count();
                if (u > 0)
                {
                    rez.Add(UserRole.AccountManager);
                }
                //var uta = utaRepo.Where(x => x.UserId == userId && !x.IsDeleted).Distinct().ToList();
                //foreach (var userToAgreenment in uta)
                //{
                //    var role = userToAgreenment.Role;
                //    if (rez.Count(x => x == role) == 0)
                //    {
                //        rez.Add(userToAgreenment.Role);
                //    }
                //}
            }
            return rez;
        }

        public string GetPasswordCode(string password, string salt)
        {
            var md5 = MD5CryptoServiceProvider.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = md5.ComputeHash(passwordBytes);

            string passwordHashString = BitConverter.ToString(passwordHash).Replace("-", String.Empty);
            
            var key = md5.ComputeHash(Encoding.UTF8.GetBytes(passwordHashString + salt));

            return BitConverter.ToString(key).Replace("-", String.Empty);


        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {


            if (Get(email, oldPassword)!=null)
            {
                var user = Get(email);

                newPassword = newPassword.Trim();
                
                user.Password = GetPasswordCode(newPassword, user.Salt);
               
                Update(user);
                
                return true;
            }
            
            return false;
        }

        public bool ResetPassword(string email, string newPassword)
        {
            var user = Get(email);

            if (user != null)
            {
                newPassword = newPassword.Trim();
                user.Password = GetPasswordCode(newPassword, user.Salt);
                Update(user);
                return true;
            }

            return false;
        }

    }
}
