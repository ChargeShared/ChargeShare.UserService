
using Microsoft.EntityFrameworkCore;

namespace ChargeShare.UserService.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
