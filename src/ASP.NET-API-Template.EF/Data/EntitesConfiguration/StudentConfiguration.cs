namespace ASP.NET_API_Template.EF.Data.Configuration;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(TablesNames.Students);


        builder.Property(e => e.FullName)
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(e => e.Email)
            .HasMaxLength(255);

        // Global filter to exclude deleted clients
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasData(SeedData.SeedStudents());
    }
}
