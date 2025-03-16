namespace ASP.NET_API_Template.API.Helper;
public static class SystemApiRouts
{
    public static class Students
    {
        public const string Base = "api/v1/Students";
        public const string GetStudentById = Base + "/{Id}";
        public const string GetAllCleint = Base;
        public const string AddStudent = Base;
        public const string UpdateStudent = Base;
        public const string DeleteStudent = Base + "/{Id}";
    }
}
