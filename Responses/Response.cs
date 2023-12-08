namespace AuthJwtServer.API.Responses
{
    public class Response
    {
        public IEnumerable<string> Errors { get; set; }
        public static Response Fail(IEnumerable<string> errorList)
        {
            return new Response { Errors = errorList };
        }
    }
}
