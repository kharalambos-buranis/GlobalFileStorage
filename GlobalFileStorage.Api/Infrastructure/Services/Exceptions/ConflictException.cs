using System.Net;

namespace GlobalFileStorage.Api.Infrastructure.Services.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string? message) : base(message) { }

        public override string Title => "Conflict";
        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    }
}
