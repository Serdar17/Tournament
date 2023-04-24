using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces;
using Tournament.Application.Interfaces.DbInterfaces;

namespace Tournament.Application.Competitions.Queries.GetCompetitionList;

public class GetCompetitionListQueryHandler : IRequestHandler<GetCompetitionInfoListQuery, CompetitionListVm>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCompetitionListQueryHandler(ICompetitionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<CompetitionListVm> Handle(GetCompetitionInfoListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Competitions
            .ProjectTo<CompetitionLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new CompetitionListVm() { Competition = entities };
    }
}