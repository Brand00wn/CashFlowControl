using Domain.Models;
using FluentValidation;
using MediatR;
using Services.Interfaces;

namespace Application.Launch.Launch.Command.DeleteLaunch;
public class DeleteProductCommandHandler(IProductService _service, IValidator<DeleteProductCommand> _validator) : IRequestHandler<DeleteProductCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ApiResponse { Success = false, Message = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage).ToList()) };
        }

        return await _service.DeleteProduct(
           request.Id
        );
    }
}