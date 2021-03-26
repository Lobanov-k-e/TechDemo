using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDemo.Core.Infrastructure.Services;
using TechDemo.Tests.Common;
using TechDemo.WebApi.Controllers;

namespace TechDemo.Tests
{
    [TestFixture]
    class UserControllerTests : TestBase
    {

        [Test]
        public async Task Index_returnsView()
        {
            var controller = GetController();

            var result = await controller.Index();

            var act = result as ViewResult;

            Assert.IsNotNull(act);
        }

        [Test]
        public async Task Index_returns_correctModel()
        {
            var controller = GetController();

            var result = (await controller.Index()) as ViewResult;

            var act = result.Model as IEnumerable<UserVm>;

            Assert.IsNotNull(act);
        }

        [Test]
        public async Task Delete_callsService_withCorrectId()
        {
            var mock = new Mock<IUserService>();
            var controller = GetController(mock);

            var expectedId = 10;

            await controller.Delete(expectedId);

            mock.Verify(m => m.DeleteAsync(It.Is<int>(val => val == expectedId)));
        }



        private UserController GetController(Mock<IUserService> service = null)
        {
            return new UserController((service ?? new Mock<IUserService>()).Object);
        }
    }
}
