using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Core.Models;
using ERP.Core.Services;
using ERP.Web.Models;
using OfficeOpenXml.Style;

namespace ERP.Web.Controllers
{
    [RoutePrefix("api/certificates")]
    public class CertificateController: BaseController
    {

        private readonly ICrudService<Certificate> certificateService;
        [NonAction]
        private JsonResult PostOrChangeCertificate(EmployeeToCertificatePoco pocoCertificate, int Id = 0)
        {
            try
            {
                certificateService.Update(new Certificate()
                {
                    ID = pocoCertificate.ID,
                    CertificateName = pocoCertificate.CertificateName
                });

                return
                    Success();
            }
            catch (Exception ex)
            {
                return
                    Error(ex.Message);
            }
        }
        public CertificateController(ICrudService<Certificate> certificateService)
        {
            this.certificateService = certificateService;
        }

        [HttpGet, Route("")]
        public JsonResult GetAllCertificates()
        {
            try
            {
                return
                    Success(
                        certificateService.GetAll()
                            .Select(c => new EmployeeToCertificatePoco() {CertificateName = c.CertificateName, ID = c.ID})
                            .ToList());
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost, Route("")]
        public JsonResult PostCertificate(EmployeeToCertificatePoco pocoCertificate)
        {
            if (!ModelState.IsValid)
            {
                var modelStatePoco = ModelStatePoco.MapFromModelState(ModelState);
                return Error(modelStatePoco);
            }

            return PostOrChangeCertificate(pocoCertificate);
        }

        [HttpPut, Route("{id}")]
        public JsonResult ChangeCertificate(int Id, EmployeeToCertificatePoco pocoCertificate)
        {
            if (!ModelState.IsValid)
            {
                var modelStatePoco = ModelStatePoco.MapFromModelState(ModelState);
                return Error(modelStatePoco);
            }

            return PostOrChangeCertificate(pocoCertificate, Id);
        }

        [HttpDelete, Route("{id}")]
        public JsonResult DeleteCertificate(int Id)
        {
            try
            {
                certificateService.Delete(Id);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}