using AutoMapper;
using FluentValidation;
using RestaurantAPI.CrossCutting.Cryptography;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Notification;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Domain.Mapper;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserCreateDTO> _validatorUserCreate;
        private readonly IValidator<UserLoginDTO> _validatorUserLogin;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository,
            INotification notification,
            IMapper mapper,
            IValidator<UserCreateDTO> validatorUserCreate,
            IValidator<UserLoginDTO> validatorUserLogin,
            ITokenService tokenService) : base(mapper, notification)
        {
            _userRepository = userRepository;
            _validatorUserCreate = validatorUserCreate;
            _validatorUserLogin = validatorUserLogin;
            _tokenService = tokenService;
        }

        public async Task<List<UserDTO>> Get() => _mapper.Map<List<UserDTO>>(await _userRepository.Get());

        public async Task<UserDTO> GetById(long id) => _mapper.Map<UserDTO>(await _userRepository.GetById(id));

        public async Task<UserCreateResponseDTO> Create(UserCreateDTO dto)
        {
            _notification.AddNotifications(await _validatorUserCreate.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            var user = _mapper.Map<User>(dto);
            user.Password = dto.Password.Crypt();
            return _mapper.Map<UserCreateResponseDTO>(await _userRepository.Add(user));
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO dto)
        {
            _notification.AddNotifications(await _validatorUserLogin.ValidateAsync(dto));
            if (_notification.HasNotifications) return null;

            var user = await _userRepository.Login(dto.Email, dto.Password.Crypt());
            if(user == null)
            {
                _notification.AddNotification("Usuário", "Usuário não encontrado!");
                return null;
            }

            var response = _mapper.Map<UserLoginResponseDTO>(user);
            response.Token = _tokenService.Generate(response);

            return response;
        }
    }
}
