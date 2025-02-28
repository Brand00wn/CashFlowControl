using Domain.Models;
using MediatR;

namespace Application.Launch.Launch.Command.DeleteLaunch;
public class DeleteProductCommand : IRequest<ApiResponse>
{
    public int Id { get; set; }
}