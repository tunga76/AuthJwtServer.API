namespace AuthJwtServer.API.Configurations
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public IEnumerable<string> Audiences { get; set; }
    }
}
