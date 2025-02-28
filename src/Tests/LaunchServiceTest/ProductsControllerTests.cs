using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Launch.Launch.Command.CreateLaunch;
using Application.Launch.Launch.Command.DeleteLaunch;
using Application.Launch.Launch.Command.UpdateLaunch;
using Application.Product.Product.Query.GetAllProducts;
using Domain.Models;
using Domain.Models.Launch.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<ProductController>> _loggerMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ProductController>>();
        _controller = new ProductController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Create_ReturnsOk_WhenProductIsValid()
    {
        var product = new ProductModel { Name = "Test Product", Price = 10.0m, Stock = 5 };
        _mediatorMock.Setup(m => m.Send(It.IsAny<ApiResponse>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var result = await _controller.Create(product);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenProductIsNull()
    {
        var result = await _controller.Create(null!);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithListOfProducts()
    {
        var products = new List<ProductModel> { new ProductModel { Name = "Product1", Price = 10, Stock = 5 } };
        _mediatorMock.Setup(m => m.Send(It.IsAny<ApiResponse>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenUpdateIsSuccessful()
    {
        var product = new ProductModel { Name = "Updated Product", Price = 15, Stock = 10 };
        _mediatorMock.Setup(m => m.Send(It.IsAny<ApiResponse>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        var result = await _controller.Update(1, product);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenProductIsNull()
    {
        var result = await _controller.Update(1, null!);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenDeleteIsSuccessful()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<ApiResponse>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenIdIsNull()
    {
        var result = await _controller.Delete(null);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
