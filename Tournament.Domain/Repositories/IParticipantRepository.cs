using Tournament.Domain.Models.Participants;

namespace Tournament.Domain.Repositories;

public interface IParticipantRepository
{
    Task<ApplicationUser?> GetParticipantByIdAsync(string id);

    void Update(ApplicationUser participant);

    void Remove(ApplicationUser participant);
}