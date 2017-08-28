using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Core.Models;
using ERP.Core.Services;
using Omu.ValueInjecter;
using ERP.Web.Models;
using ERP.Web.Mappers;
using ERP.SqlServer;
using ERP.Services;
using ERP.Core;

namespace ERP.Web.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHistoryService _historyService;

        public EmployeeController(IEmployeeService employeeService, IHistoryService historyService)
        {
            this._employeeService = employeeService;
            this._historyService = historyService;
        }

        [HttpGet, Route("{id}")]
        public ActionResult GetEmployee(int id)
        {
            var employee = _employeeService.Get(id);
            if (employee == null)
                throw new HttpException(404, "Employee not found");
            var employeePoco = employee.MapToEmployeePoco();
            return Success(employeePoco);
        }

        [HttpPut, Route("{id}")]
        public ActionResult UpdateEmployee(EmployeePoco employeePoco, int id)
        {
            if (ModelState.IsValid)
            {
                var employee = employeePoco.MapToEmployee();
                int employeeId = this._employeeService.Update(employee);
                return Success(employeeId);
            }
            else
            {
                return Error(ModelStatePoco.MapFromModelState(ModelState));
            }
        }

        [HttpDelete, Route("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            _employeeService.Delete(id);
            return Success();
        }

        [HttpPost, Route("")]
        public ActionResult CreateEmployee(EmployeePoco employeePoco)
        {
            if (ModelState.IsValid)
            {
                var employee = employeePoco.MapToEmployee();
                employee.ID = 0;
                int employeeId = this._employeeService.Update(employee);
                return Success(employeeId);
            }
            else
            {
                return Error(ModelStatePoco.MapFromModelState(ModelState));
            }
        }

        [HttpGet, Route("")]
        public ActionResult GetAllEmployees()
        {
            var employees = _employeeService.GetAll().Select(e => e.MapToEmployeePoco());
            return Success(employees);
        }

        [HttpGet, Route("{id}/shorthistory")]
        public JsonResult GetShortHistory(int id)
        {
            var employee = _employeeService.Get(id);
            if (employee == null)
                throw new HttpException(404, "Epmployee not found.");
            return Success(_historyService.GetShortHistoryById(id));
        }

        [HttpGet, Route("{id}/history")]
        public JsonResult GetHistory(int id)
        {
            var employee = _employeeService.Get(id);
            if (employee == null)
                throw new HttpException(404, "Epmployee not found.");
            return Success(_historyService.GetHistoryById(id));
        }
    }
}