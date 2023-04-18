using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Common.Exceptions;
using Tournament.Application.Competitions.Queries.GetCompetitionInfoDetail;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Queries.GetCompetitionInfoDetails;

public class GetCompetitionInfoDetailsQueryHandler : IRequestHandler<GetCompetitionInfoDetailsQuery, CompetitonInfoVm>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCompetitionInfoDetailsQueryHandler(ICompetitionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<CompetitonInfoVm> Handle(GetCompetitionInfoDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.CompetitionInfos
            .FirstOrDefaultAsync(item => item.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(CompetitionInfo), request.Id);
        }

        return _mapper.Map<CompetitonInfoVm>(entity);
    }
}