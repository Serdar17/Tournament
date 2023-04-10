using Tournament.AppDbContext;
using Tournament.Models;
using Tournament.Services;

namespace Tournament.Implementation;

public class ParticipantRepository : IParticipantRepository<Participant>
{
    private readonly ApplicationDbContext _dbContext;

    public ParticipantRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<Participant> GetAll()
    {
        return _dbContext.Participants;
    }

    public Participant Create(Participant participant)
    {
        var entity = _dbContext.Add(participant).Entity;
        _dbContext.SaveChanges();
        return entity;
    }
}