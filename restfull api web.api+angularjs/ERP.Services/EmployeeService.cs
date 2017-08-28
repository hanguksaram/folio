using ERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Core.Repository;
using ERP.Core.Services;
using System.Linq.Expressions;
using ERP.Core;
using ERP.Core.HistoryStuff;
using Newtonsoft.Json;


namespace ERP.Services
{

    public class EmployeeService : CrudService<Employee>, IEmployeeService
    {
        public IHistoryService historyService { get; }
        public EmployeeService(IRepo<Employee> employeeRepo, IHistoryService hs) : base(employeeRepo)
        {
            this.historyService = hs;
        }

        public override int Update(Employee employee)
        {
            if (employee.ID == 0)
            {
                var newEmployee = this.Repo.Insert(employee);
                this.Repo.Save();
                historyService.Update(newEmployee.ID);
                return newEmployee.ID;
            }
            var updatedEmployee = this.Get(employee.ID);
            if (updatedEmployee == null)
                return 0;

            updatedEmployee.BirthDate = employee.BirthDate;
            updatedEmployee.FiredDate = employee.FiredDate;
            updatedEmployee.FirstName = employee.FirstName;
            updatedEmployee.HiringDate = employee.HiringDate;
            updatedEmployee.LastName = employee.LastName;
            updatedEmployee.MiddleName = employee.MiddleName;
            updatedEmployee.PositionId = employee.PositionId;
            
            this.UpdateSkills(updatedEmployee, employee.Skills);
            this.UpdateCertificates(updatedEmployee, employee.Certificates);
            this.UpdateProperties(updatedEmployee, employee.Properties);

            this.Repo.Save();
            historyService.Update(updatedEmployee.ID);
            return updatedEmployee.ID;
        }

        private void UpdateSkills(Employee updatedEmployee, ICollection<EmployeeToSkill> skillsFrom)
        {
            // edit exist skill and remove
            var removeList = new List<EmployeeToSkill>();
            foreach (var employeeSkillTo in updatedEmployee.Skills)
            {
                bool isExist = false;
                foreach (var employeeSkillFrom in skillsFrom)
                {
                    if (employeeSkillTo.ID == employeeSkillFrom.ID)
                    {
                        employeeSkillTo.Level = employeeSkillFrom.Level;
                        employeeSkillTo.Preference = employeeSkillFrom.Preference;
                        employeeSkillTo.SkillId = employeeSkillFrom.SkillId;
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                    removeList.Add(employeeSkillTo);
            }
            foreach (var emloyeeSkill in removeList)
            {
                updatedEmployee.Skills.Remove(emloyeeSkill);
            }
            // add new skill
            foreach (var skillFrom in skillsFrom)
            {
                var isExist = false;
                foreach (var skillTo in updatedEmployee.Skills)
                {
                    if (skillTo.SkillId == skillFrom.SkillId)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                    continue;
                var newSkill = new EmployeeToSkill
                {
                    Level = skillFrom.Level,
                    Preference = skillFrom.Preference,
                    SkillId = skillFrom.SkillId
                };
                updatedEmployee.Skills.Add(newSkill);
            }
        }

        private void UpdateCertificates(Employee updatedEmployee, ICollection<EmployeeToCertificate> certsFrom)
        {
            // edit exist certificates and remove
            var removeList = new List<EmployeeToCertificate>();
            foreach (var employeeCertTo in updatedEmployee.Certificates)
            {
                bool isExist = false;
                foreach (var employeeCertFrom in certsFrom)
                {
                    if (employeeCertTo.ID == employeeCertFrom.ID)
                    {
                        employeeCertTo.Comment = employeeCertFrom.Comment;
                        employeeCertTo.ExpireDate = employeeCertFrom.ExpireDate;
                        employeeCertTo.ReceiptDate = employeeCertFrom.ReceiptDate;
                        employeeCertTo.Pending = employeeCertFrom.Pending;
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                    removeList.Add(employeeCertTo);
            }
            foreach (var emloyeeCert in removeList)
            {
                updatedEmployee.Certificates.Remove(emloyeeCert);
            }
            // add new certificates
            foreach (var certFrom in certsFrom)
            {
                if (certFrom.ID == 0)
                {
                    updatedEmployee.Certificates.Add(certFrom);
                }
            }
        }

        private void UpdateProperties(Employee updatedEmployee, ICollection<Property> propsFrom)
        {
            // edit exist properties and remove
            var removeList = new List<Property>();
            foreach (var employeePropTo in updatedEmployee.Properties)
            {
                bool isExist = false;
                foreach (var employeePropFrom in propsFrom)
                {
                    if (employeePropTo.ID == employeePropFrom.ID)
                    {
                        employeePropTo.PropertyValue = employeePropFrom.PropertyValue;
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                    removeList.Add(employeePropTo);
            }
            foreach (var emloyeeProp in removeList)
            {
                updatedEmployee.Properties.Remove(emloyeeProp);
            }
            // add new properties
            foreach (var propFrom in propsFrom)
            {
                if (propFrom.ID == 0)
                {
                    updatedEmployee.Properties.Add(propFrom);
                }
            }
        }

        public IList<SendingHistoryPoco> GetShortHistory(int id)
        {
            return this.historyService.GetShortHistoryById(id);
        }
    }
}
