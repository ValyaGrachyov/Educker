using Contract.ClientDto;

namespace Services.Abstraction;

public interface ICommentsService
{
    public Task<IEnumerable<CommentsDto>> GetAllAsync();
}