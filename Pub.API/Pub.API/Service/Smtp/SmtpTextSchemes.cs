namespace Pub.API.Service.Smtp {
    public static class SmtpTextSchemes {
        public static class Subject {

            public static string MakeAssignmentAfterSubject(string teamName) {
                return string.Format("Kocsmkavíz:Sikeres csapatjelentkezés ({0})",teamName);
            }

            public static string MakeAssignmentHandledSubject(string teamName) {
                return string.Format("<Kocsmakviz:Jelentkezési státusz változása! ({0})",teamName);
            }
        }

        public static class Body {
            public static string MakeAssignmentAfterBody(string teamName) {
                return string.Format("<h2>Kedves {0}!<p>Jelentkezés feldolgozás alatt van!</p>",teamName);
            }

            public static string MakeAssignmentHandledBody(string teamName, bool isAccepted) {
                return string.Format("<h2>Kedves {0}!<p>Csapatjelentkezés {0}!</p>",isAccepted ? "Elfogadva" : "Elutasítva");
            }
        }
    }
}
