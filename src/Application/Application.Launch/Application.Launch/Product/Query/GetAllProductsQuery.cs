using Domain.DTOs.Launch;
using MediatR;

namespace Application.Product.Product.Query.GetAllProducts;

public class GetAllProductsQuery : IRequest<List<ProductDTO>>
{
}