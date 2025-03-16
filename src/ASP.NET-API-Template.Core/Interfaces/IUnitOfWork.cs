namespace ASP.NET_API_Template.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository Students { get; }
    Task<int> SaveAsync();

}

