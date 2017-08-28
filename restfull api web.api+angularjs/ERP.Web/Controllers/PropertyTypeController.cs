using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Core.Models;
using ERP.Core.Services;
using ERP.Web.Mappers;
using ERP.Web.Models;

namespace ERP.Web.Controllers
{
    [RoutePrefix("api/propertytype")]
    public class PropertyTypeController : BaseController
    {
        private readonly ICrudService<PropertyType> _propertyTypeService;

        public PropertyTypeController(ICrudService<PropertyType> propertyTypeService)
        {
            this._propertyTypeService = propertyTypeService;
        }

        [HttpGet, Route("")]
        public ActionResult GetAllPropertyTypes()
        {
            try
            {
                var allPropertyTypesPoco = _propertyTypeService
                    .GetAll()
                    .Select(t => t.MapToPropertyTypePoco())
                    .ToList();

                return Success(allPropertyTypesPoco);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpGet, Route("{id}")]
        public ActionResult GetOnePropertyType(int id)
        {
            try
            {
                var propertyTypePoco = _propertyTypeService
                    .Get(id)
                    .MapToPropertyTypePoco();

                return Success(propertyTypePoco);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost, Route("")]
        public ActionResult PostOnePropertyType(PropertyTypePoco propertyTypePoco)
        {
            if (!ModelState.IsValid)
            {
                var modelStatePoco = ModelStatePoco.MapFromModelState(ModelState);
                return Error(modelStatePoco);
            }

            try
            {
                var propertyType = propertyTypePoco.MapToPropertyType();

                _propertyTypeService.Update(propertyType);

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPut, Route("{id}")]
        public ActionResult PutOnePropertyType(int id, PropertyTypePoco propertyTypePoco)
        {
            if (!ModelState.IsValid)
            {
                var modelStatePoco = ModelStatePoco.MapFromModelState(ModelState);
                return Error(modelStatePoco);
            }

            try
            {
                var propertyType = propertyTypePoco.MapToPropertyType();

                _propertyTypeService.Update(propertyType);

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpDelete, Route("{id}")]
        public ActionResult DeleteOnePropertyType(int id)
        {
            try
            {
                _propertyTypeService.Delete(id);

                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}