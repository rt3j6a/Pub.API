namespace Pub.API.Common {
    public static class Router {

        public const string VersionOne = "V1";
        public const string Admin = "Admin";
        public const string User = "User";

        public static class Authentication {
            public const string PrefixName = "Authentication";

            public const string Login = "Login";
            public const string TestToken = "TestToken";
            public const string AddAccount = "AddAccount";
        }

        public static class Team {
            public const string PrefixName = "Team";

            public const string AddTeamAssignment = "AddTeamAssignment";
            public const string DeleteTeamAssignment = "DeleteTeamAssignment";
            public const string GetTeamAssignments = "GetTeamAssignments";
            public const string HandleTeamAssignment = "HandleTeamAssignment";
        }
    }
}
