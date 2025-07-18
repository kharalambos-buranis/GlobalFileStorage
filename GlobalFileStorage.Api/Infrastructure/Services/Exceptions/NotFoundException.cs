using System.Net;

namespace GlobalFileStorage.Api.Infrastructure.Services.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string? message) : base(message)
        {
        }

        public override string Title => "Not found";
        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
