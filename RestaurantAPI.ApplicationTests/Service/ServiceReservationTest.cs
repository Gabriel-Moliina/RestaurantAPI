using AutoMapper;
using Moq;
using RestaurantAPI.Domain.Builder.ReservationBuilder;
using RestaurantAPI.Domain.DTO.Reservation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Builder;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Service.Services;

namespace RestaurantAPI.ServiceTests.Service
{
    public class ServiceReservationTest
    {
        private readonly Mock<IMapper> _mockAutoMapper;
        private readonly Mock<IReservationRepository> _mockReservationRespository;
        private readonly Mock<IReservationBuilder> _mockReservationBuilder;
        private readonly Mock<ITableReservationResponseBuilder> _mockTableReservationResponseBuilder;
        private readonly Mock<ITableRepository> _mockTableRepository;
        private readonly IReservationService _reservationService;
        public ServiceReservationTest()
        {
            _mockReservationRespository = new Mock<IReservationRepository>();
            _mockAutoMapper = new Mock<IMapper>();
            _mockTableRepository = new Mock<ITableRepository>();
            _mockTableReservationResponseBuilder = new Mock<ITableReservationResponseBuilder>();
            _mockReservationBuilder = new Mock<IReservationBuilder>();

            _reservationService = new ReservationService(_mockAutoMapper.Object,
                _mockTableRepository.Object,
                _mockReservationBuilder.Object,
                _mockTableReservationResponseBuilder.Object,
                _mockReservationRespository.Object);
        }

        [Fact]
        public async Task TestCreate_Success()
        {
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "teste restaurant"
            };

            var table = new Table
            {
                Id = 1,
                Capacity = 1,
                Identification = "22",
                RestaurantId = 1,
                Reserved = false,
                Restaurant = restaurant
            };

            var reservationBuilder = new ReservationBuilder();
            var tableReservationResponseBuilder = new TableReservationResponseBuilder();

            var reservationDto = new TableReservationDTO
            {
                Date = DateTime.MinValue,
                Email = "teste@hotmail.com",
                TableId = 1
            };

            var reservation = new Reservation
            {
                TableId = reservationDto.TableId,
                Date = reservationDto.Date,
                Email = reservationDto.Email
            };

            var tableReservationResponseDTO = new TableReservationResponseDTO
            {
                Date = reservationDto.Date,
                Email = reservationDto.Email,
                Identification = table.Identification,
                Reserved = table.Reserved,
                RestaurantName = table.Restaurant.Name,
            };

            _mockTableRepository.Setup(c => c.GetById(1)).ReturnsAsync(table).Verifiable();

            _mockReservationBuilder.Setup(builder => builder.WithEmail(reservationDto.Email)).Returns(reservationBuilder);
            _mockReservationBuilder.Setup(builder => builder.WithDate(reservationDto.Date)).Returns(reservationBuilder);
            _mockReservationBuilder.Setup(builder => builder.WithTableId(reservationDto.TableId)).Returns(reservationBuilder);

            _mockTableReservationResponseBuilder.Setup(builder => builder.WithDate(It.IsAny<DateTime>())).Returns(tableReservationResponseBuilder);
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithEmail(It.IsAny<string>())).Returns(tableReservationResponseBuilder);
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithIdentification(It.IsAny<string>())).Returns(tableReservationResponseBuilder);
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithReserved(It.IsAny<bool>())).Returns(tableReservationResponseBuilder);
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithRestaurantName(It.IsAny<string>())).Returns(tableReservationResponseBuilder);

            var response = await _reservationService.Create(reservationDto);

            Assert.True(response.Reserved);

            _mockTableRepository.Verify();
        }

        [Fact]
        public async Task TestCreate_Error_WhenTableIsNull()
        {
            var reservationDto = new TableReservationDTO
            {
                Date = DateTime.Now,
                Email = "teste@hotmail.com",
                TableId = 1
            };

            _mockTableRepository.Setup(c => c.GetById(1)).ReturnsAsync((Table)null).Verifiable();

            var response = await _reservationService.Create(reservationDto);

            Assert.Null(response);

            _mockTableRepository.Verify();
        }
    }
}
