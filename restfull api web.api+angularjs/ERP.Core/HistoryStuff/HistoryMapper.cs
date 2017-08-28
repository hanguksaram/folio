using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Models;

namespace ERP.Core.HistoryStuff
{
    public static class HistoryMapper
    {
        public static HistoryPoco MapToEmployeeHistory(this Employee employee)
        {
            if (employee == null)
                return null;

            var employeePoco = new HistoryPoco()
            {
                FiredDate = employee.FiredDate,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PositionName = employee.Position.PositionName,
            };
            employeePoco.Skills = new List<HsSkillPoco>();
            if (employee.Skills != null)
            {
                foreach (var skill in employee.Skills)
                {
                    employeePoco.Skills.Add(new HsSkillPoco
                    {
                        Level = (int)skill.Level,
                        Preference = skill.Preference.ToString(),
                        SkillName = skill.Skill.SkillName,
                    });
                }
            }
            employeePoco.Certificates = new List<HsCertificatePoco>();
            if (employee.Certificates != null)
            {
                
                foreach (var certificate in employee.Certificates)
                {
                    employeePoco.Certificates.Add(new HsCertificatePoco
                    {
                        ID = certificate.ID,
                        CertificateName = certificate.Certificate.CertificateName,
                        Comment = certificate.Comment,
                        ExpireDate = certificate.ExpireDate,
                        ReceiptDate = certificate.ReceiptDate,
                        Pending = certificate.Pending
                    });
                }
            }
            employeePoco.Properties = new List<HsPropertyPoco>();
            if (employee.Properties != null)
            {
                
                foreach (var property in employee.Properties)
                {
                    employeePoco.Properties.Add(new HsPropertyPoco
                    {
                        PropertyType = property.PropertyType.PropertyTypeName,
                        PropertyValue = property.PropertyValue
                    });
                }
            }
            return employeePoco;
        }
    }
}

