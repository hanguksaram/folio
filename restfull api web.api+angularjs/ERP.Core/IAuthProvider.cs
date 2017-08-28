namespace ERP.Core
{
    public interface IAuthProvider
    {
        string CurrentUser { get; set; }
        bool IsAuth { get; set; }
        int UserId { get; set; }
    }
}