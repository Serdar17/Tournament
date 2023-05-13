using Ardalis.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.Create;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Competitions.Commands.DeleteCompetition;

public class DeleteCompetitionHandler : ICommandHandler<DeleteCompetitionCommand, CompetitionListVm>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeleteCompetitionHandler(ICompetitionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<CompetitionListVm>> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Competitions.FindAsync(new object[] {request.Id},
            cancellationToken);

        if (entity is null)
        {
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.Id}) was not found.");
            // throw new NotFoundException(nameof(Competition), request.Id);
        }

        _dbContext.Competitions.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var entities = await _dbContext.Competitions
            .ProjectTo<CompetitionLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result.Success(new CompetitionListVm() { Competition = entities });
    }
}