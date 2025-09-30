using Moq;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Validator.User;

namespace RestarauntAPI.UserServiceTest
{
    public class UserServiceValidatorTest
    {
        [Fact]
        public async void Create_WithInvalidEmail_ShouldFail()
        {
            //Arrange
            var email = new UserCreateDTO
            {
                Email = "test.com",
                Password = "Password",
                ConfirmPassword = "Password"
            };

            var mockUserRepository = new Mock<IUserRepository>();
            var validator = new UserCreateValidator(mockUserRepository.Object);
            mockUserRepository.Setup(r => r.Exists(email.Email)).ReturnsAsync(false);


            //Act
            var result = await validator.ValidateAsync(email);

            //Assert
            Assert.False(result.IsValid);
        }
    }
}