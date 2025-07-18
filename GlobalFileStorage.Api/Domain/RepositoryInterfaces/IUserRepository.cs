using GlobalFileStorage.Api.Common.Entities;

namespace GlobalFileStorage.Api.Domain.RepositoryInterfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
