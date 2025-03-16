namespace ASP.NET_API_Template.EF.Repositories;

public class UnitOfWork(AppDbContext _appDbContext) : IUnitOfWork
{
    public IStudentRepository Students { get; private set; } = new StudentRepository(_appDbContext);



    public void Dispose()
    {
        _appDbContext.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}
