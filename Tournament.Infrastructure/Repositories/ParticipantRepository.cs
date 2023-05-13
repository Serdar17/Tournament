using Microsoft.AspNetCore.Identity;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly UserManager<ApplicationUser> _manager;

    public ParticipantRepository(UserManager<ApplicationUser> manager)
    {
        _manager = manager;
    }

    public async Task<ApplicationUser?> GetParticipantByIdAsync(string id)
    {
        return await _manager.FindByIdAsync(id);
    }

    public async void Update(ApplicationUser participant)
    {
        await _manager.UpdateAsync(participant);
    }

    public async void Remove(ApplicationUser participant)
    {
        await _manager.DeleteAsync(participant);
    }
}