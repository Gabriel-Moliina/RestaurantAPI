using FluentValidation;
using RestaurantAPI.Domain.DTO.Table;
using RestaurantAPI.Domain.Interface.Repository;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Domain.Validator.Table
{
    public class TableDeleteValidator : AbstractValidator<TableDeleteDTO>
    {
        private readonly ITableRepository _tableRepository;
        private readonly ITokenService _tokenService;
        public TableDeleteValidator(ITableRepository tableRepository,
            ITokenService tokenService
            )
        {
            _tableRepository = tableRepository;
            _tokenService = tokenService;

            RuleFor(a => a.Id)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _tableRepository.ExistsByIdAndUserId(id, _tokenService.GetUserId);
                })
                .WithMessage("Mesa não encontrada");
        }
    }
}
