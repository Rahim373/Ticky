using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;
using Ticky.Shared.ViewModels.Auth;

namespace Ticky.Application.Commands.Auth;

public record GetTokenCommand(string Email, string Password) : ITickyRequest<TokenResponse>;

public class GetTokenValidator : AbstractValidator<GetTokenCommand>
{
    public GetTokenValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty();
    }
}

public class GetTokenCommandHandler : ITickyRequestHandler<GetTokenCommand, TokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthService _authService;

    public GetTokenCommandHandler(
        IUserRepository userRepository,
        SignInManager<ApplicationUser> signInManager,
        IAuthService authService)
    {
        _userRepository = userRepository;
        _signInManager = signInManager;
        _authService = authService;
    }

    public async Task<ErrorOr<TokenResponse>> Handle(GetTokenCommand request, CancellationToken cancellationToken)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

        if (!signInResult.Succeeded)
        {
            return Error.Validation(description: "Email or password invalid.");
        }

        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        var roles = await _signInManager.UserManager.GetRolesAsync(user!);
        var applicationRoles = await _userRepository.GetAllRolesAsync();

        var refreshToken = await _authService.GenerateRefreshTokenAsync(cancellationToken);
        var (accessToken, expiryDate) = await _authService.GenerateAccessTokenAsync(user!, roles, cancellationToken);

        var authenticatedUser = new User(user!.Id, user.Email!, roles);
        var response = new TokenResponse(
            accessToken,
            refreshToken,
            expiryDate,
            authenticatedUser,
            applicationRoles.Select(x => x.ToRole()).ToArray());
        return await Task.FromResult(response);
    }
}