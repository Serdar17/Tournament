using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces;

namespace Tournament.Application.Competitions.Queries.GetCompetitionInfoList;

public class GetCompetitionInfoListQueryHandler : IRequestHandler<GetCompetitionInfoListQuery, CompetitionInfoListVm>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCompetitionInfoListQueryHandler(ICompetitionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<CompetitionInfoListVm> Handle(GetCompetitionInfoListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.CompetitionInfos
            .ProjectTo<CompetitionInfoLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new CompetitionInfoListVm() { CompetitionInfos = entities };
    }
}