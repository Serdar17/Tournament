using Tournament.Models;
using Tournament.Primitives;

namespace Tournament.Services;

public interface IParticipantRepository<T> where  T : BaseEntity
{
    public IEnumerable<T> GetAll();

    public Participant Create(T participant);
}