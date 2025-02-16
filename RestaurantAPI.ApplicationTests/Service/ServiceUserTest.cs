using AutoMapper;
using Moq;
using RestaurantAPI.CrossCutting.Cryptography;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Service.Services;

namespace RestaurantAPI.ServiceTests.Application
{
    public class ServiceUserTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<IMapper> _mockAutoMapper;
        private readonly IUserService _userService;

        public ServiceUserTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _mockAutoMapper = new Mock<IMapper>();

            _userService = new UserService(_mockUserRepository.Object,
                _mockAutoMapper.Object,
                _mockTokenService.Object);
        }

        [Fact]
        public async Task TestLogin_Success()
        {
            var user = new User
            {
                Email = "molina@molina.com",
                Password = "Molina123".Crypt(),
                Id = 1
            };

            var userLoginResponseDTO = new UserLoginResponseDTO
            {
                Email = user.Email,
                Id = user.Id,
                Token = "123456e"
            };

            var userDTO = new UserLoginDTO
            {
                Email = "molina@molina.com",
                Password = "Molina123".Crypt()
            };

            string token = "123456e";

            _mockTokenService.Setup(c => c.Generate(It.IsAny<UserLoginResponseDTO>())).Returns(token).Verifiable();
            _mockAutoMapper.Setup(c => c.Map<UserLoginResponseDTO>(It.IsAny<User>())).Returns(userLoginResponseDTO).Verifiable();
            _mockUserRepository.Setup(c => c.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            var response = await _userService.Login(userDTO);

            Assert.Equal(1, response?.Id);
            Assert.NotEmpty(response?.Token);

            _mockTokenService.Verify();
            _mockAutoMapper.Verify();
            _mockUserRepository.Verify();
        }

        [Fact]
        public async Task TestLogin_WhenUserIsNull_Erro()
        {
            var userDTO = new UserLoginDTO
            {
                Email = "molina@molina.com",
                Password = "Molina123"
            };

            _mockAutoMapper.Setup(c => c.Map<UserLoginResponseDTO>(null)).Returns((UserLoginResponseDTO)null).Verifiable();
            _mockUserRepository.Setup(c => c.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User)null).Verifiable();

            var response = await _userService.Login(userDTO);

            Assert.Null(response);

            _mockTokenService.Verify(x => x.Generate(It.IsAny<UserLoginResponseDTO>()), Times.Never());
            _mockTokenService.Verify();
            _mockAutoMapper.Verify();
            _mockUserRepository.Verify();
        }
    }
}