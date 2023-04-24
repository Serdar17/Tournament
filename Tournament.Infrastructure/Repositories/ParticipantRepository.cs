using Microsoft.AspNetCore.Identity;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly UserManager<Participant> _manager;

    public ParticipantRepository(UserManager<Participant> manager)
    {
        _manager = manager;
    }

    public async Task<Participant?> GetParticipantByIdAsync(string id)
    {
        return await _manager.FindByIdAsync(id);
    }

    public async void Update(Participant participant)
    {
        await _manager.UpdateAsync(participant);
    }

    public async void Remove(Participant participant)
    {
        await _manager.DeleteAsync(participant);
    }
}