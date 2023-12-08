using AuthJwtServer.API.Dtos;
using AuthJwtServer.API.Models;
using AuthJwtServer.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthJwtServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly UserManager<AppUser> userManager;

        public TokenController(ITokenService tokenService, UserManager<AppUser> userManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<TokenDto> CreateToken([FromBody]UserDto user)
        {
            var appUser = new AppUser() {
                Email = user.Email,
                Id = user.Id,
                UserName = user.UserName
            };
            var localUser = await userManager.FindByEmailAsync(appUser.Email); 
            if (localUser == null) throw new Exception("Kullanıcı bulunamadı");
            return await tokenService.CreateToken(appUser);
        }
    }
}
