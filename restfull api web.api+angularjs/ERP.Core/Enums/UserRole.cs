using System.ComponentModel;

namespace ERP.Core.Enums
{
    //todo: remove user role from ERP if it's not used and completely replaced by enum UserType
    public enum UserRole
    {
        [Description("Product Specialist")]
        Spesialist = 0,
        [Description("Project Manager")]
        ProjectManager = 1,
        [Description("Account Manager")]
        AccountManager = 2,
        [Description("Accountant")]
        Accountant = 3,
        [Description("Team Leader")]
        TeamLeader = 4,        
        [Description("Developer")]
        Developer = 5
    }
}