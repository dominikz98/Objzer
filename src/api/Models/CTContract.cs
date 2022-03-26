using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models
{
    public abstract class CTContract : CTEntity
    {
        public IList<CTObject> Objects { get; set; } = new List<CTObject>();
    }

    internal class CTContractConfig : IEntityTypeConfiguration<CTContract>
    {
        public void Configure(EntityTypeBuilder<CTContract> builder)
            => builder.ToTable("contracts");
    }

    internal static class CTContractExtensions
    {
        internal static void ApplyCTContract(this ModelBuilder builder)
            => builder.ApplyConfiguration(new CTContractConfig());
    }
}
