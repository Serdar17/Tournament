using Tournament.Domain.Models.Participants;

namespace Tournament.Domain.Repositories;

public interface IParticipantRepository
{
    Task<Participant?> GetParticipantByIdAsync(string id);

    void Update(Participant participant);

    void Remove(Participant participant);
}