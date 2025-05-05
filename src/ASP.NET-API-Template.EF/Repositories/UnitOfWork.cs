using Microsoft.Extensions.Logging;

namespace ASP.NET_API_Template.EF.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }


    private IStudentRepository _studentRepository;
    public IStudentRepository Students => _studentRepository ??=
        new StudentRepository(_context, _serviceProvider.GetRequiredService<ILogger<StudentRepository>>());

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
