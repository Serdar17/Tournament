using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Infrastructure.EntityTypeConfigurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        builder.OwnsOne(x => x.FirstPlayerScored);
        builder.OwnsOne(x => x.SecondPlayerScored);
    }
}