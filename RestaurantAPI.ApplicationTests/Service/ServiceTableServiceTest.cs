using AutoMapper;
using Moq;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Service.Services;

namespace RestaurantAPI.ServiceTests.Service
{
    public class ServiceTableServiceTest
    {
        private readonly Mock<IMapper> _mockAutoMapper;
        private readonly Mock<ITableRepository> _mockTableRepository;
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly ITableService _tableService;
        public ServiceTableServiceTest()
        {
            _mockAutoMapper = new Mock<IMapper>();
            _mockTableRepository = new Mock<ITableRepository>();
            _mockReservationRepository = new Mock<IReservationRepository>();
            _mockTokenService = new Mock<ITokenService>();

            _tableService = new TableService(_mockAutoMapper.Object,
                _mockTableRepository.Object,
                _mockReservationRepository.Object);
        }
        [Fact]
        public async Task TesteSaveOrUpdate_Create_Success()
        {
            var tableSaveDTO = new TableSaveDTO
            {
                Capacity = 100,
                Identification = "2",
                RestaurantId = 1
            };

            var table = new Table
            {
                Capacity = 100,
                Identification = "2",
                RestaurantId = 1,
                Id = 0
            };

            var tableResponseDto = new TableResponseDTO
            {
                RestaurantId = 1,
                Identification = "2",
                Capacity = 100,
                Id = 1
            };

            _mockAutoMapper.Setup(c => c.Map<TableResponseDTO>(It.IsAny<Table>())).Returns(tableResponseDto).Verifiable();
            _mockAutoMapper.Setup(c => c.Map<Table>(It.IsAny<TableSaveDTO>())).Returns(table).Verifiable();

            var tableResponseService = await _tableService.SaveOrUpdate(tableSaveDTO);

            Assert.Equal(1, tableResponseService.Id);

            _mockAutoMapper.Verify();
            _mockTableRepository.Verify();
            _mockTableRepository.Verify(c => c.GetById(It.IsAny<long>()), Times.Never());
        }
    }
}
