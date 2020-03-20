using Library.Controllers;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.Controllers
{
    public class UserUnitTests
    {
        [Fact]
        public async Task GetUsers_200Ok() {
            var m = new Mock<IUserRepository>();

            ICollection<User> users = new List<User>
            {
                new User{ IdUser=1, Name="abc", Email="abc@net.com" },
                new User{ IdUser=2, Name="def", Email="def@net.com" },
                new User{ IdUser=3, Name="ghi", Email="ghi@net.com" }

            };

            m.Setup(c => c.GetUsers()).Returns(Task.FromResult(users));
            var controller = new UsersController(m.Object);

            var result = await controller.GetUsers();

            Assert.True(result is OkObjectResult);
            var r = result as OkObjectResult;
            Assert.True((r.Value as ICollection<User>).Count == 3);
            
        }
    }
}
