namespace ERP.SqlServer
{
    using System.Data.Entity;

    public interface IDbContextFactory
    {
        DbContext GetContext();
    }
}