using CompanyRatingApi.Application.Authentication.Handlers.Login;
using CompanyRatingApi.Application.Authentication.Handlers.Register;
using CompanyRatingApi.Shared.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CompanyRatingApi.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class AuthController(
    LoginHandler loginHandler,
    RegisterHandler registerHandler
) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<ActionResult<AccessTokenDto>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await registerHandler.Handle(request, cancellationToken));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AccessTokenDto>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await loginHandler.Handle(request, cancellationToken));
    }
}