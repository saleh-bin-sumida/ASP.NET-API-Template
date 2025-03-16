namespace ASP.NET_API_Template.Core.Entites;


public class Student : IEntity, ISoftDeletable
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
