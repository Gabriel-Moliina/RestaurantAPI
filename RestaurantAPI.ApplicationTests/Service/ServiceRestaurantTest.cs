using AutoMapper;
using Moq;
using RestaurantAPI.Domain.DTO.Restaurant;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Service.Services;

namespace RestaurantAPI.ServiceTests.Service
{
    public class ServiceRestaurantTest
    {
        private readonly Mock<IRestaurantRepository> _mockRestaurantRepository;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<IMapper> _mockAutoMapper;
        private readonly IRestaurantService _restaurantRervice;
        public ServiceRestaurantTest()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _mockAutoMapper = new Mock<IMapper>();
            _restaurantRervice = new RestaurantService(_mockRestaurantRepository.Object,
                _mockTokenService.Object,
                _mockAutoMapper.Object);
        }

        [Fact]
        public async Task TestSaveOrUpdate_Create_Erro_WhenRestaurantIsNull()
        {
            var restauranteCreate = new RestaurantSaveDTO
            {
                Name = "restaurante teste",
            };

            _mockAutoMapper.Setup(c => c.Map<Restaurant>(restauranteCreate)).Returns((Restaurant)null);
            var restaurant = await _restaurantRervice.SaveOrUpdate(restauranteCreate);

            Assert.Null(restaurant);
        }
    }
}
