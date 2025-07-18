using System.Net;

namespace GlobalFileStorage.Api.Infrastructure.Services.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string? message) : base(message)
        {
        }

        public abstract string Title { get; }
        public abstract HttpStatusCode StatusCode { get; }
    }
}
