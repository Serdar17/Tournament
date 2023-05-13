using Ardalis.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.Create;
using Tournament.Application.Interfaces.DbInterfaces;

namespace Tournament.Application.Competitions.Queries.GetCompetitionList;

public class GetCompetitionListQueryHandler : IQueryHandler<GetCompetitionInfoListQuery, CompetitionListVm>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCompetitionListQueryHandler(ICompetitionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Result<CompetitionListVm>> Handle(GetCompetitionInfoListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Competitions
            .ProjectTo<CompetitionLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new CompetitionListVm() { Competition = entities };
    }
}