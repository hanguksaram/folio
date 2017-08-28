using ERP.Core.Enums;
using ERP.Core.Models;
using ERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Mappers
{
    public static class EmployeeMapper
    {
        public static Employee MapToEmployee(this EmployeePoco employeePoco)
        {
            if (employeePoco == null)
                return null;
            var employee = new Employee {
                BirthDate = employeePoco.BirthDate,
                FiredDate = employeePoco.FiredDate,
                FirstName = employeePoco.FirstName,
                HiringDate = employeePoco.HiringDate,
                ID = employeePoco.ID,
                LastName = employeePoco.LastName,
                MiddleName = employeePoco.MiddleName,
                PositionId = employeePoco.Position.ID,
            };
            employee.Skills = new List<EmployeeToSkill>();
            if (employeePoco.Skills != null)
            {
                foreach (var skill in employeePoco.Skills)
                {
                    employee.Skills.Add(new EmployeeToSkill {
                        ID = skill.ID,
                        Level = (Level)skill.Level,
                        Preference = (Preference)skill.Preference,
                        SkillId = skill.SkillId,
                    });
                }
            }
            employee.Certificates = new List<EmployeeToCertificate>();
            if (employeePoco.Certificates != null)
            {
                foreach (var certificate in employeePoco.Certificates)
                {
                    employee.Certificates.Add(new EmployeeToCertificate {
                        ID = certificate.ID,
                        CertificateId = certificate.CertificateId,
                        Comment = certificate.Comment,
                        ExpireDate = certificate.ExpireDate,
                        ReceiptDate = certificate.ReceiptDate,
                        Pending = certificate.Pending
                    });
                }
            }
            employee.Properties = new List<Property>();
            if (employeePoco.Properties != null)
            {
                foreach (var property in employeePoco.Properties)
                {
                    employee.Properties.Add(new Property {
                        ID = property.ID,
                        PropertyTypeId = property.PropertyTypeId,
                        PropertyValue = property.PropertyValue
                    });
                }
            }
            return employee;
        }

        public static EmployeePoco MapToEmployeePoco(this Employee employee)
        {
            var employeePoco = new EmployeePoco {
                BirthDate = employee.BirthDate,
                FiredDate = employee.FiredDate,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                HiringDate = employee.HiringDate,
                ID = employee.ID,
                Position = new PositionPoco {
                    ID = employee.Position.ID,
                    PositionName = employee.Position.PositionName,
                }
            };
            employeePoco.Skills = new List<EmployeeToSkillPoco>();
            if (employee.Skills != null)
            {
                foreach (var skill in employee.Skills)
                {
                    employeePoco.Skills.Add(new EmployeeToSkillPoco {
                        ID = skill.ID,
                        Level = (int)skill.Level,
                        Preference = (int)skill.Preference,
                        SkillName = skill.Skill.SkillName,
                        SkillId = skill.Skill.ID,
                    });
                }
            }
            employeePoco.Certificates = new List<EmployeeToCertificatePoco>();
            if (employee.Certificates != null)
            {
                foreach (var certificate in employee.Certificates)
                {
                    employeePoco.Certificates.Add(new EmployeeToCertificatePoco {
                        ID = certificate.ID,
                        CertificateId = certificate.Certificate.ID,
                        CertificateName = certificate.Certificate.CertificateName,
                        Comment = certificate.Comment,
                        ExpireDate = certificate.ExpireDate,
                        ReceiptDate = certificate.ReceiptDate,
                        Pending = certificate.Pending
                    });
                }
            }
            employeePoco.Properties = new List<PropertyPoco>();
            if (employee.Properties != null)
            {
                foreach (var property in employee.Properties)
                {
                    employeePoco.Properties.Add(new PropertyPoco {
                        ID = property.ID,
                        PropertyType = property.PropertyType.PropertyTypeName,
                        PropertyTypeId = property.PropertyType.ID,
                        PropertyValue = property.PropertyValue
                    });
                }
            }
            return employeePoco;
        }
    }
}