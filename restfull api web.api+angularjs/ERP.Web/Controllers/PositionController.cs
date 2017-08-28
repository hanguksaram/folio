using ERP.Core.Models;
using ERP.Core.Services;
using ERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.ValueInjecter;

namespace ERP.Web.Controllers
{
    [RoutePrefix("api/positions")]
    public class PositionController : BaseController
    {
        private readonly ICrudService<Position> positionService;
        public PositionController(ICrudService<Position> positionService)
        {
            this.positionService = positionService;
        }

        [HttpGet, Route("")]
        public ActionResult GetAllPositions()
        {
            return Success(positionService.GetAll().Select(p => new PositionPoco().InjectFrom(p)));
        }

        [HttpGet, Route("{id}")]
        public ActionResult GetPosition(int id)
        {
            var position = positionService.Get(id);
            if (position == null)
                throw new HttpException(404, "Position not found.");
            return Success(new PositionPoco().InjectFrom(position));
        }

        [HttpPost, Route("")]
        public ActionResult CreatePosition(PositionPoco positionPoco)
        {
            positionPoco.ID = 0;
            if (ModelState.IsValid)
                return Success(positionService.Update(new Position().InjectFrom(positionPoco) as Position));
            else
                return Error(ModelStatePoco.MapFromModelState(ModelState));
        }

        [HttpPut, Route("{id}")]
        public ActionResult UpdatePosition(PositionPoco positionPoco, int id)
        {
            var position = positionService.Get(id);
            if (position == null)
                throw new HttpException(404, "Position not found.");
            if (ModelState.IsValid)
                return Success(positionService.Update(new Position().InjectFrom(positionPoco) as Position));
            else
                return Error(ModelStatePoco.MapFromModelState(ModelState));
        }

        [HttpDelete, Route("{id}")]
        public ActionResult DeletePosition(int id)
        {
            positionService.Delete(id);
            return Success();
        }
    }
}