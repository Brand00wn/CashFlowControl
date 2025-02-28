﻿using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Authentication.Command.Register;
public class RegisterCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
