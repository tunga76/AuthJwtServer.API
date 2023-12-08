namespace AuthJwtServer.API.Dtos
{
    public class ClientTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccesTokenExpiration { get; set; }
    }
}