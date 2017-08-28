using System.Web;

namespace ERP.Web.Context
{
    using ERP.Core.Models;
    using ERP.Services;
    using ERP.SqlServer;

    public class AppContext
    {
        private const string ContextKey = "ERPContext";

        public static ERPContext Current
        {
            get
            {
                var cnt = HttpContext.Current.Session[ContextKey] as ERPContext;
                if (cnt == null)
                {
                    var builder = new AppContext();
                    cnt = builder.Create();
                    if (cnt.IsAuth)
                    {
                        HttpContext.Current.Session.Add(ContextKey, cnt);
                    }

                    return cnt;
                }

                return cnt;
            }
        }

        public static void ClearContext()
        {
            HttpContext.Current.Session[ContextKey] = null;
        }

        public static void UpdateContext()
        {
            var builder = new AppContext();
            var cnt = builder.Create();
            HttpContext.Current.Session.Add(ContextKey, cnt);
        }        

        private ERPContext Create()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return new ERPContext { IsAuth = false };
            }
            var repo = new Repo<User>(new DbContextFactory(), new FakeAuth());
            var userService = new UserService(repo);
            var user = userService.Get(HttpContext.Current.User.Identity.Name);
            if (user != null)
            {

                return new ERPContext { IsAuth = true, Login = user.Email, FirstName = user.FirstName, LastName = user.LastName, IsAdmin = user.IsSa, UserId = user.ID};
            }

            return new ERPContext();
        }

    }
}