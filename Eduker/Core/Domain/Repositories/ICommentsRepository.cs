using Domain.Entities;

namespace Domain.Repositories;

public interface ICommentsRepository
{
    public Task<IEnumerable<Comments>> GetAllAsync();
}