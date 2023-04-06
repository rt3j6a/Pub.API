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
            public const string GetAllTeamAssignments = "GetAllTeamAssignments";
            public const string GetActiveTeamAssignments = "GetAcitveTeamAssignments";
            public const string HandleTeamAssignment = "HandleTeamAssignment";
            public const string UpdateTeamAssignmentStatus = "UpdateTeamAssignmentStatus";
        }

        public static class Event {
            public const string PrefixName = "Event";

            public const string GetAllEvents = "GetAllEvents";
            public const string GetActiveEvents = "GetActiveEvents";
            public const string AddEvent = "AddEvent";
            public const string GetEvent = "GetEvent";
            public const string UpdateEventStatus = "UpdateEventStatus";
        }
    }
}
