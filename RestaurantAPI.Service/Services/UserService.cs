using AutoMapper;
using FluentValidation;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Mapper;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserCreateDTO> _validatorUserCreate;

        public UserService(IUserRepository userRepository,
            INotification notification,
            IMapper mapper,
            IValidator<UserCreateDTO> validatorUserCreate) : base(mapper, notification)
        {
            _userRepository = userRepository;
            _validatorUserCreate = validatorUserCreate;
        }

        public async Task<List<UserDTO>> Get() => _mapper.Map<List<UserDTO>>(await _userRepository.Get());

        public async Task<UserDTO> GetById(long id) => _mapper.Map<UserDTO>(await _userRepository.GetById(id));

        public async Task<UserCreateResponseDTO> Create(UserCreateDTO dto)
        {
            _notification.AddNotifications(await _validatorUserCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            var user = _mapper.Map<User>(dto);
            return _mapper.Map<UserCreateResponseDTO>(await _userRepository.Create(user));
        }
    }
}
