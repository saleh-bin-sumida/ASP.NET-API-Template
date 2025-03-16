namespace ASP.NET_API_Template.Core.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    DateTime? DeletedDate { get; set; }

}
