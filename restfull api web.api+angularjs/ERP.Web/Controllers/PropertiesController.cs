using System;
using System.Collections.Generic;
using System.Linq;
using System.util;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ERP.Core.Models;
using ERP.Core.Services;
using ERP.Web.Mappers;
using ERP.Web.Models;
using Omu.ValueInjecter;

namespace ERP.Web.Controllers
{
    [RoutePrefix("api/properties")]
    public class PropertiesController : BaseController
    {
        private readonly ICrudService<Property> _propertiesService;

        public PropertiesController(ICrudService<Property> propertiesService)
        {
            this._propertiesService = propertiesService;
        }

        [HttpGet, Route("")]
        public ActionResult GetAllProperties()
        {
            try
            {
                var allPropertiesPoco = _propertiesService
                    .GetAll()
                    .Select(s => s.PropertyType.MapToPropertyTypePoco())
                    .ToList();

                return Success(allPropertiesPoco);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpGet, Route("{id}")]
        public ActionResult GetOneProperty(int id)
        {
            try
            {
                var propertyPoco = _propertiesService
                    .Get(id)
                    .MapToPropertyPoco();

                return Success(propertyPoco);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost, Route("")]
        public ActionResult PostOneProperty(PropertyPoco propertyPoco)
        {
            if (!ModelState.IsValid)
            {
                var modelStatePoco = ModelStatePoco.MapFromModelState(ModelState);
                return Error(modelStatePoco);
            }

            try
            {
                var property = propertyPoco.MapToProperty();

                _propertiesService.Update(property);

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPut, Route("{id}")]
        public ActionResult PutOneProperty(int id, PropertyPoco propertyPoco)
        {
            if (!ModelState.IsValid)
            {
                var modelStatePoco = ModelStatePoco.MapFromModelState(ModelState);
                return Error(modelStatePoco);
            }

            try
            {
                var property = propertyPoco.MapToProperty();

                _propertiesService.Update(property);

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpDelete, Route("{id}")]
        public ActionResult DeleteOneProperty(int id)
        {
            try
            {
                _propertiesService.Delete(id);

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}