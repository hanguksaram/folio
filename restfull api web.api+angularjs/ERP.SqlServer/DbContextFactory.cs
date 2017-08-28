using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SqlServer
{
    using System.Data.Entity;

    public class DbContextFactory : IDbContextFactory, IDisposable
    {
        //if we are correctly setup per request life time manager - that would do the trick

        private DbContext ctx;

        public DbContext GetContext()
        {
            return ctx ?? (ctx = new Db());            
        }


        public void Dispose()
        {
            if (ctx != null)
            {
                ctx.Dispose();
            }
        }
    }
}
