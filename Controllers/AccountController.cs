using System.Linq;
using Fisher.Bookstore.Api.Data;
using Fisher.Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Fisher.Bookstore.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuation = configuration;
        }
    }

    [HttpPost("register")]
    public async Task<IActionsResult> Registry([FromBody] ApplicationUser registration)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        ApplicationUser user = new ApplicationUser
        {
            Email = registration.Email,
            UserName = registration.Email,
            Id = registration.Email
        };

        IdentityResult result = await userManager.CreateAsync(user, registration.Password);

        if (!result.Succeeded))
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(err.Code, err.Description);
            }

            return BadRequest(ModelState);
        }
        return Ok();
    }
    

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ApplicationUser login)
    {
        var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);
        if(!result.Succeeded)
        {
            return Unauthorized();
        }

        ApplicationUser user = await userManager.FindByEmailAsync(login.Email);
        JwtSecurityToken token = await GenerateTokenAsync(user);
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
       
        var response = new { Token = serializedToken };
        return Ok(response);
    }

    private async Task<JwtSecurityToken> GenerateTokenAsync(AppIicationUser user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.Guid().toString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };
        var expirationDays = configuration.GetValue<int>("JwtConfiguration:TokenExpirationDays");
        var signingKey = Encoding.UTF8.GetGytes(IDesignTimeMvcBuilderConfiguration.GetValue<string>("JwtConfiguration:Key"));
        var token = new JwtSecurityToken(
            Issuer: configuration.GetValue<string>("JWTConfiguration:Issuer"),
            audience: configuration.GetValue<string>("JWTConfiguration:Audience"), 
            claims: claims, 
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(expirationDays)),
            notbefore: DateTime.UtcNow, 
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256));

        return token;
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return OkObjectResult(User.Identity.Name);
    }

    }
    
