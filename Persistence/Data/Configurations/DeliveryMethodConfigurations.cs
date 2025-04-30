namespace Persistence.Data.Configurations;

public class DeliveryMethodConfigurations
                    : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable("DeliveryMethods");
        builder.Property(d => d.Price)
            .HasColumnType("decimal(18,2)");
    }
}