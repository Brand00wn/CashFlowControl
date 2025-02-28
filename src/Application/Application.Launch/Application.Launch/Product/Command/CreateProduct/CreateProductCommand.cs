using Domain.Models;
using MediatR;

namespace Application.Launch.Launch.Command.CreateLaunch;
public class CreateProductCommand : IRequest<ApiResponse>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}