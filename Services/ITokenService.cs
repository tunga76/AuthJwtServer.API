using AuthJwtServer.API.Configurations;
using AuthJwtServer.API.Dtos;
using AuthJwtServer.API.Models;

namespace AuthJwtServer.API.Services
{
    public interface ITokenService
    {
        ClientTokenDto CreateClientToken(Client client);
        Task<TokenDto> CreateToken(AppUser appUser);
    }
}