using Ardalis.Result;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Application.Interfaces;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Queries.GetCompetitionDetails;

public class GetCompetitionDetailsQueryHandler : IQueryHandler<GetCompetitionDetailsQuery, CompetitionVm>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCompetitionDetailsQueryHandler(ICompetitionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Result<CompetitionVm>> Handle(GetCompetitionDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Competitions
            .FirstOrDefaultAsync(item => item.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Competition), request.Id);
        }

        return Result.Success(_mapper.Map<CompetitionVm>(entity));
    }
}