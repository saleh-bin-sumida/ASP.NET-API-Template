using Mapster;

namespace ASP.NET_API_Template.Core.Mapping
{
    public static class MappingConfig
    {
        public static void ConfigMapster(this IServiceCollection services)
        {
            TypeAdapterConfig<Student, StudentDto>.NewConfig()
                .Map(dest => dest.FullName, src => src.FirstName + " " + src.LastName);



        }
    }
}
