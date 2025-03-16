using ASP.NET_API_Template.EF.Data.Configuration;

namespace ASP.NET_API_Template.EF.Data;

public class AppDbContext : DbContext
{
    #region DbSets

    public DbSet<Student> Students { get; set; }

    #endregion

    #region Constructors

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    #endregion

    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly);
    }

    #endregion
}

