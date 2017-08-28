using System.ComponentModel;

namespace ERP.Core.Enums
{
    public enum UserType
    {
        [Description("System Admin")]
        SA = 0,
        [Description("Manager")]
        Manager = 1,
        [Description("Employee")]
        Employee = 2
    }
}