namespace AuthJwtServer.API.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefresToken { get; set; }
        public DateTime AccesTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
