
using System;
using System.Collections.Generic;
using System.Linq;
using ERP.Core.Models;
using ERP.Core.Services;
using ERP.Services.Mocks;
using ERP.Web.Controllers;
using ERP.Web.Models;
using NUnit.Framework;

namespace ERP.UnitTests
{
    [TestFixture]
    public class SkillControllerTests : BaseTestClass
    {
        private SkillsController _controller;
        private ICrudService<Skill> _mockSkills;


        [SetUp]
        public void SetUpController()
        {
            this._mockSkills = new MockService<Skill>();
            this._controller = new SkillsController(this._mockSkills);
            base.SetControllerMocks(this._controller);
        }

        [Test]
        public void GetAllSkills_ShouldBeSuccessfull()
        {
            var result = this._controller.GetAllSkills().Data;

            var success = Convert.ToBoolean(GetFieldFromAnonymous(result, "success"));

            Assert.AreEqual(success, true);
        }

        [Test]
        public void GetAllSkills_ShouldGetAllSkills()
        {
            var result = this._controller.GetAllSkills().Data;

            var data = GetFieldFromAnonymous(result, "response") as List<EmployeeToSkillPoco>;
            var trueData = this._mockSkills.GetAll();

            Assert.AreEqual(data.Count, trueData.Count());

            for (int i = 0; i < data.Count; i++)
            {
                var toCompare = trueData.FirstOrDefault(x => x.ID == data[i].ID);

                Assert.AreEqual(data[i].ID, toCompare.ID);
                Assert.AreEqual(data[i].SkillName, toCompare.SkillName);
            }
        }
    }
}
