namespace SchoolPrj.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "api";
        public const string virsion = "v1";
        public const string baseUrl = root + "/" + virsion + "/";

        public class Student
        {
            public const string controller = "Student";
            public const string prefix = baseUrl + controller;
            public const string getList = prefix + "/List";
            public const string getById = prefix + "/{id}";
            public const string create = prefix + "/Create";
            public const string edit = prefix + "/Edit";
            public const string delete= prefix + "/{id}";
            public const string paginatedList = prefix + "/PaginatedList";

        }
        public class Department
        {
            public const string controller = "Department";
            public const string prefix = baseUrl + controller;
            public const string getList = prefix + "/List";
            public const string getById = prefix + "/{id}";
            public const string create = prefix + "/Create";
            public const string edit = prefix + "/Edit";
            public const string delete= prefix + "/{id}";
            public const string paginatedList = prefix + "/PaginatedList";
        }

        public class User
        {
            public const string controller = "User";
            public const string prefix = baseUrl + controller;
            public const string create = prefix + "/Create";
            public const string edit = prefix + "/Edit";
            public const string delete= prefix + "/{id}";
            public const string paginatedList = prefix + "/PaginatedList";
            public const string getById = prefix + "/{id}";
        }
    }
}
