using Application.Authentication.Authentication.Command.ChangePassword;
using Application.Authentication.Authentication.Command.Login;
using Application.Authentication.Authentication.Command.Register;
using Domain.Models;
using Domain.Models.Authentication;
using Domain.Models.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

public class AuthenticationControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<AuthenticationController>> _loggerMock;
    private readonly Mock<SignInManager<ApplicationUserModel>> _signInManagerMock;
    private readonly AuthenticationController _controller;

    public AuthenticationControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<AuthenticationController>>();

        var userManagerMock = new Mock<UserManager<ApplicationUserModel>>(
            Mock.Of<IUserStore<ApplicationUserModel>>(), null!, null!, null!, null!, null!, null!, null!, null!
        );

        _signInManagerMock = new Mock<SignInManager<ApplicationUserModel>>(
            userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUserModel>>(),
            null!, null!, null!, null!
        );

        _controller = new AuthenticationController(_mediatorMock.Object, _signInManagerMock.Object, _loggerMock.Object);
    }

    // Login Test
    [Fact]
    public async Task Login_ValidCredentials_ReturnsOkWithToken()
    {
        // Arrange
        var loginDto = new LoginModel { Username = "testuser", Password = "password123" };
        var loginCommand = new LoginCommand { Username = loginDto.Username, Password = loginDto.Password };
        var expectedToken = "mocked-jwt-token";

        var response = new LoginResponseModel { Success = true, Token = expectedToken };

        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResponse = Assert.IsType<LoginResponseModel>(okResult.Value);

        Assert.True(actualResponse.Success);
        Assert.Equal(expectedToken, actualResponse.Token);
    }


    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginDto = new LoginModel { Username = "testuser", Password = "wrongpassword" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new UnauthorizedAccessException());

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Invalid credentials.", unauthorizedResult.Value);
    }

    [Fact]
    public async Task Login_NullRequest_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.Login(new LoginModel());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid login request.", badRequestResult.Value);
    }

    // Register Test
    [Fact]
    public async Task Register_ValidUser_ReturnsOk()
    {
        // Arrange
        var registerCommand = new RegisterCommand { UserName = "newuser", FullName = "Teste", Password = "Password123!" };
        var response = new ApiResponse { Success = true };

        _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

        // Act
        var result = await _controller.Register(registerCommand);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task Register_Failed_ReturnsBadRequest()
    {
        // Arrange
        var registerCommand = new RegisterCommand { UserName = "newuser", FullName = "Teste", Password = "weak" };
        var response = new ApiResponse { Success = false, Message = "Weak password" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

        // Act
        var result = await _controller.Register(registerCommand);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(response, badRequestResult.Value);
    }

    // ChangePassword Test
    [Fact]
    public async Task ChangePassword_ValidRequest_ReturnsOk()
    {
        // Arrange
        var changePasswordCommand = new ChangePasswordCommand
        {
            Username = "username",
            CurrentPassword = "OldPass123!",
            NewPassword = "NewPass456!"
        };

        var response = new ApiResponse { Success = true };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

        // Act
        var result = await _controller.ChangePassword(changePasswordCommand);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task ChangePassword_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var changePasswordCommand = new ChangePasswordCommand
        {
            Username = "username",
            CurrentPassword = "OldPass123!",
            NewPassword = "weak"
        };

        var response = new ApiResponse { Success = false, Message = "Weak password" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(response);

        // Act
        var result = await _controller.ChangePassword(changePasswordCommand);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(response, badRequestResult.Value);
    }

    [Fact]
    public async Task ChangePassword_ThrowsException_ReturnsServerError()
    {
        // Arrange
        var changePasswordCommand = new ChangePasswordCommand
        {
            Username = "username",
            CurrentPassword = "OldPass123!",
            NewPassword = "NewPass456!"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), It.IsAny<CancellationToken>()))
                     .ThrowsAsync(new System.Exception("Unexpected error"));

        // Act
        var result = await _controller.ChangePassword(changePasswordCommand);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("An error occurred while processing your request.", objectResult.Value);
    }

    // Logout Test
    [Fact]
    public async Task Logout_CallsSignOutAsync()
    {
        // Act
        await _controller.LogoutAsync();

        // Assert
        _signInManagerMock.Verify(s => s.SignOutAsync(), Times.Once);
    }
}
