using AutoMapper;
using RestaurantAPI.CrossCutting.Cryptography;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Services;
using RestaurantAPI.Domain.Interface.Token;
using RestaurantAPI.Service.Services.Base;

namespace RestaurantAPI.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ITokenService tokenService) : base(mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserCreateResponseDTO> Create(UserCreateDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Password = dto.Password.Crypt();
            return _mapper.Map<UserCreateResponseDTO>(await _userRepository.Add(user));
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO dto)
        {
            var user = await _userRepository.ValidateUser(dto.Email, dto.Password);
            var response = _mapper.Map<UserLoginResponseDTO>(user);

            if(response != null) response.Token = _tokenService.Generate(response);

            return response;
        }
    }
}
