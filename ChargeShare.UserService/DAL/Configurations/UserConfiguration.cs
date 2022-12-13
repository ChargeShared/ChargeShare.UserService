
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ChargeShare.UserService.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ChargeSharedUserModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ChargeSharedUserModel> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
