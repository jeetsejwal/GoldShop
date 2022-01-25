using GoldShop.Controllers;
using GoldShop.Infrastructure;
using GoldShop.Infrastructure.Dtos;
using GoldShop.Infrastructure.IShopServices;
using GoldShop.Infrastructure.ShopServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace GoldShopTest
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly UsersController _controller;
        private readonly IUsersServices _service;
        private readonly IConfiguration _config;
        protected GoldDBContext context;

        public UserControllerTest()
        {
            if (_service == null)
                _service = new UsersServices(context);
            _controller = new UsersController(_service, _config);
        }

        //------------using moq
        public Mock<IUsersServices> usrServices = new Mock<IUsersServices>();
        [Fact]
        public void LoginTest()
        {
            var usrDto = new UsersDto();
            var dto = new LoginDto()
            {
                UserName = "Rkumar",  Password = "1234"
            };
            usrServices.Setup(p => p.LoginUser(dto)).ReturnsAsync(usrDto);
            UsersController usrController = new UsersController(usrServices.Object, _config);
            var result = usrController.Login(dto);
        }

        [TestMethod]
        internal void DiscountCalculatorTest()
        {
            var dto = new GoldCalculatorDto()
            {
                GoldAmount = 1000, GoldWeight = 20, Discount = 10
            };
            var resultCalculator = _controller.DiscountCalculator(dto);
            var okResult = Xunit.Assert.IsType<OkObjectResult>(resultCalculator.Result);
            Xunit.Assert.NotNull(okResult);
            Xunit.Assert.Equal(200, okResult.StatusCode);
        }

    }
}
