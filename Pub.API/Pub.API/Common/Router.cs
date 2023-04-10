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
            public const string GetTeamAssignment = "GetTeamAssignment";
        }

        public static class Event {
            public const string PrefixName = "Event";

            public const string GetAllEvents = "GetAllEvents";
            public const string GetActiveEvents = "GetActiveEvents";
            public const string AddEvent = "AddEvent";
            public const string GetEvent = "GetEvent";
            public const string UpdateEventStatus = "UpdateEventStatus";
        }

        public static class Table {
            public const string PrefixName = "Table";

            public const string GetAllTables = "GetAllTables";
            public const string AddTable = "AddTable";
            public const string DeleteTable = "DeleteTable";

            public const string AddTableReservation = "AddTableReservation";
            public const string GetTableReservation = "GetTableReservation";
            public const string DeleteTableReservation = "DeleteTableReservation";
            public const string UpdateTableReservationComment = "UpdateTableReservationComment";
            public const string DeleteAllTableReservationForEvent = "DeleteAllTableReservationForEvent";
            public const string GetallTableReservations = "GetallTableReservations";

        }

        public static class Post {
            public const string PrefixName = "Post";

            public const string AddPost = "AddPost";
            public const string DeletePost = "DeletePost";
            public const string UpdatePostContent = "UpdatePostContent";
            public const string GetAllPosts = "GetAllPosts";
            public const string GetAllPostsByEvent = "GetAllPostsByEvent";
            public const string GetPost = "GetPost";

        }

        public static class Result {
            public const string PrefixName = "Result";

            public const string AddResult = "AddResult";
            public const string RemoveResult = "RemoveResult";
            public const string UpdateResultScore = "UpdateResultScore";
            public const string GetAllResults = "GetAllResults";
            public const string GetAllResultsByEvent = "GetAllResultsByEvent";
            public const string GetResult = "GetResult";


        }
    }
}
