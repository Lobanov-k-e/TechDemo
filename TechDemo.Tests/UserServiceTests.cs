using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechDemo.Core.Infrastructure.Services;
using TechDemo.Tests.Common;

namespace TechDemo.Tests
{
    [TestFixture]
    class UserServiceTests : TestBase
    {

        [Test]
        public async Task GetAll_getsAll()
        {
            var service = GetUserService();
            var expectedCount = await Context.Users.CountAsync();

            var act = await service.GetAllAsync();

            Assert.AreEqual(expectedCount, act.Count());
        }

        [Test]
        public async Task CreateUser_canCreate()
        {
            var mock = new Mock<IFileService>();      
            mock.Setup(m => m.SaveFile(It.IsAny<IFormFile>())).ReturnsAsync("testPath");

            var model = new CreateUserVm
            {
                Name = "testName",
                Email = "test@test.com",
                Password = "testpass",
                DateOfBirth = DateTime.MaxValue,
                Photo = It.IsAny<IFormFile>()
            };

            var expectedCount = await Context.Users.CountAsync() + 1;

            await (GetUserService(mock).CreateAsync(model));

            Assert.AreEqual(expectedCount, await Context.Users.CountAsync());
        }

        [Test]
        public async Task UpdateUser_notCallSaveFile_ifPhotoIsNull()
        {
            var mock = new Mock<IFileService>();
            var model = new UpdateUserVm
            {
                Id = (await Context.Users.FirstAsync()).Id,             
                Photo =  null
            };

            var service = GetUserService(mock);

            await service.UpdateAsync(model);

            mock.Verify(m => m.SaveFile(It.IsAny<IFormFile>()), Times.Never);
        }


        private IUserService GetUserService(Mock<IFileService> mock = null)
        {            
            return new UserService(base.Context, base.Mapper, (mock ?? new Mock<IFileService>()).Object);
        }
    }
}
