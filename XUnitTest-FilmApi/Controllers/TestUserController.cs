using JLFilmApi.Controllers;
using JLFilmApi.Repo.Contracts;
using JLFilmApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_FilmApi.Controllers
{
    public class TestUserController
    {
        private readonly Mock<IUserRepository> mockUsersRepo;
        private readonly UsersController usersController;

        public TestUserController()
        {
            mockUsersRepo = new Mock<IUserRepository>();
            usersController = new UsersController(mockUsersRepo.Object);
        }

        [Fact]
        public void AddNewUser_Test()
        {
            var user = new AddViewUsers { Name = "TestUser", Surname = "Surname", Login = "Login", Password = "password", AccountImage = "non.png" };
            var result = usersController.AddNewUser(user);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Invalid_AddNewUser_Test()
        {
            var user = new AddViewUsers { Name = "TestUser", Surname = "Surname", Login = "Login", AccountImage = "non.png" };
            usersController.ModelState.AddModelError("Password", "Required");
            var badResponse = usersController.AddNewUser(user);
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }
    }
}
