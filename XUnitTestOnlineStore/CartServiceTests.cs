using Microsoft.AspNetCore.Http;
using Moq;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Repositories;
using System.Threading.Tasks;
using Xunit;
using XUnitTestOnlineStore.Extensions;

namespace XUnitTestOnlineStore
{
    public class CartServiceTests
    {
        #region CONFIG
        private readonly ICartService cartServiceTests;
        private readonly Mock<IHttpContextAccessor> httpContextAccessorMock;
        private readonly Mock<ICartRepository> cartRepositoryMock;

        public CartServiceTests()
        {
            httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            cartRepositoryMock = new Mock<ICartRepository>();
            cartServiceTests = new CartService(httpContextAccessorMock.Object, cartRepositoryMock.Object);
        }
        #endregion

        [Fact]
        public async Task GetCartAsync_ShopCartCookieExists_ShouldReturnCartFromDatabase()
        {
            //Arrange
            var token = "secrettoken123";
            var cookie = MockCookieCollection.RequestCookieCollection("ShopCart", token);

            cartRepositoryMock.Setup(c => c.FindByTokenAsync(token)).Returns(Task.FromResult(new Cart()));
            httpContextAccessorMock.Setup(s => s.HttpContext.Request.Cookies).Returns(cookie);

            //Act
            var result = await cartServiceTests.GetCartAsync();

            //Assert
            cartRepositoryMock.Verify(c => c.FindByTokenAsync(token), Times.Once);
            cartRepositoryMock.Verify(c => c.CreateCartAsync(), Times.Never);
            httpContextAccessorMock.Verify(h =>
                h.HttpContext.Response.Cookies.Append("ShopCart", token), Times.Never);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCartAsync_ShopCartCookieNotExists_ShouldCreateCartInDatabaseAndReturnIt()
        {
            //Arrange
            var token = "secrettoken123";
            var cookie = MockCookieCollection.RequestCookieCollection(null, null);

            cartRepositoryMock.Setup(c => c.CreateCartAsync()).Returns(Task.FromResult(new Cart(){ Token = token }));
            httpContextAccessorMock.Setup(s => s.HttpContext.Request.Cookies).Returns(cookie);
            httpContextAccessorMock.Setup(s => 
                s.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));

            //Act
            var result = await cartServiceTests.GetCartAsync();

            //Assert
            cartRepositoryMock.Verify(c => c.FindByTokenAsync(It.IsAny<string>()), Times.Never);
            cartRepositoryMock.Verify(c => c.CreateCartAsync(), Times.Once);
            httpContextAccessorMock.Verify(h =>
                h.HttpContext.Response.Cookies.Append("ShopCart", token), Times.Once);

            Assert.NotNull(result);
        }
    }
}
