namespace Persistence.Data.Configurations;

public class OrderItemConfigurations
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems") ;
        builder.Property(d => d.Price)
            .HasColumnType("decimal(8,2)");

        builder.OwnsOne(o => o.Product, builder => builder.WithOwner());
    }
}