using AuthJwtServer.API.Configurations;
using AuthJwtServer.API.Dtos;
using AuthJwtServer.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthJwtServer.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOption custonTokenOption;

        public TokenService(IOptions<CustomTokenOption> custonTokenOption)
        {
            this.custonTokenOption = custonTokenOption.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
        private IEnumerable<Claim> GetClaims(AppUser appUser, List<string> audiences)
        {
            var userClaims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            userClaims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userClaims;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,client.Id)
            };
            claims.AddRange(client.Audiences.Select(s => new Claim(JwtRegisteredClaimNames.Aud, s)));
            return claims;
        }

        public Task<TokenDto> CreateToken(AppUser appUser)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(custonTokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(custonTokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(custonTokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: custonTokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: GetClaims(appUser, custonTokenOption.Audiences), signingCredentials: signingCredentials);
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto()
            {
                AccessToken = token,
                RefresToken = CreateRefreshToken(),
                AccesTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };
            return Task.FromResult(tokenDto);
        }

        public ClientTokenDto CreateClientToken(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(custonTokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(custonTokenOption.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: custonTokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: GetClaimsByClient(client), signingCredentials: signingCredentials);
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto()
            {
                AccessToken = token,
                AccesTokenExpiration = accessTokenExpiration,
            };
            return tokenDto;
        }
    }
}
