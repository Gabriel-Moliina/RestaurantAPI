using AutoMapper;
using Moq;
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
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly Mock<IReservationBuilder> _mockReservationBuilder;
        private readonly Mock<ITableReservationResponseBuilder> _mockTableReservationResponseBuilder;
        private readonly Mock<ITableRepository> _mockTableRepository;
        private readonly IReservationService _reservationService;
        public ServiceReservationTest()
        {
            _mockReservationRepository = new Mock<IReservationRepository>();
            _mockAutoMapper = new Mock<IMapper>();
            _mockTableRepository = new Mock<ITableRepository>();
            _mockTableReservationResponseBuilder = new Mock<ITableReservationResponseBuilder>();
            _mockReservationBuilder = new Mock<IReservationBuilder>();

            _reservationService = new ReservationService(_mockAutoMapper.Object,
                _mockTableRepository.Object,
                _mockReservationBuilder.Object,
                _mockTableReservationResponseBuilder.Object,
                _mockReservationRepository.Object);
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
                Reserved = true,
                Restaurant = restaurant
            };

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

            _mockTableRepository.Setup(c => c.GetById(It.IsAny<long>())).ReturnsAsync(table).Verifiable();

            _mockReservationBuilder.Setup(builder => builder.WithEmail(It.IsAny<string>())).Returns(_mockReservationBuilder.Object).Verifiable();
            _mockReservationBuilder.Setup(builder => builder.WithDate(It.IsAny<DateTime>())).Returns(_mockReservationBuilder.Object).Verifiable();
            _mockReservationBuilder.Setup(builder => builder.WithTableId(It.IsAny<long>())).Returns(_mockReservationBuilder.Object).Verifiable();
            _mockReservationBuilder.Setup(builder => builder.Build()).Returns(reservation).Verifiable();

            SetupTableReservationResponseBuilder();

            var response = await _reservationService.Create(reservationDto);

            Assert.True(response.Reserved);

            _mockTableRepository.Verify();
            _mockReservationBuilder.Verify();
            _mockTableReservationResponseBuilder.Verify();
        }
        [Fact]
        public async Task TestCreate_WhenTableIsNull_Error()
        {
            var reservationDto = new TableReservationDTO
            {
                Date = DateTime.Now,
                Email = "teste@hotmail.com",
                TableId = 1
            };

            _mockTableRepository.Setup(c => c.GetById(It.IsAny<long>())).ReturnsAsync((Table)null).Verifiable();

            var response = await _reservationService.Create(reservationDto);

            Assert.Null(response);

            _mockTableRepository.Verify();
        }
        [Fact]
        public async Task TestCancel_Success()
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

            var reservation = new Reservation
            {
                TableId = table.Id,
                Date = DateTime.MinValue,
                Email = "teste@teste.com"
            };

            _mockTableRepository.Setup(c => c.GetById(It.IsAny<long>())).ReturnsAsync(table).Verifiable();
            _mockReservationRepository.Setup(c => c.GetByTableId(It.IsAny<long>())).ReturnsAsync(reservation).Verifiable();

            SetupTableReservationResponseBuilder();

            var response = await _reservationService.Cancel(It.IsAny<long>());

            Assert.False(response.Reserved);

            _mockTableRepository.Verify();
            _mockReservationRepository.Verify();
            _mockTableReservationResponseBuilder.Verify();
        }
        [Fact]
        public async Task TestCancel_GetTableById_WhenTableIsNull_Error()
        {
            _mockTableRepository.Setup(builder => builder.GetById(It.IsAny<long>())).ReturnsAsync((Table)null).Verifiable();

            var response = await _reservationService.Cancel(It.IsAny<long>());

            Assert.Null(response);

            _mockReservationRepository.Verify(x => x.GetByTableId(It.IsAny<long>()), Times.Never());
            _mockTableRepository.Verify();
        }

        private TableReservationResponseDTO CreateTableReservationResponseDTO() =>
            new TableReservationResponseDTO
            {
                Date = DateTime.MinValue,
                Email = "teste@teste.com",
                Identification = "123",
                Reserved = true,
                RestaurantName = "Restaurant Name"
            };

        private void SetupTableReservationResponseBuilder()
        {
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithDate(It.IsAny<DateTime>())).Returns(_mockTableReservationResponseBuilder.Object).Verifiable();
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithEmail(It.IsAny<string>())).Returns(_mockTableReservationResponseBuilder.Object).Verifiable();
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithIdentification(It.IsAny<string>())).Returns(_mockTableReservationResponseBuilder.Object).Verifiable();
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithReserved(false)).Returns(_mockTableReservationResponseBuilder.Object).Verifiable();
            _mockTableReservationResponseBuilder.Setup(builder => builder.WithRestaurantName(It.IsAny<string>())).Returns(_mockTableReservationResponseBuilder.Object).Verifiable();
            _mockTableReservationResponseBuilder.Setup(builder => builder.Build()).Returns(CreateTableReservationResponseDTO()).Verifiable();
        }
    }
}
