using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Common {
    public static class Messages {
        public static class Team {
            public const string TeamAlreadyRegistered = "Ilyen csapat már regisztrálva van ezzel a névvel!";
            public const string TeamMemberCountInvalid = "Érvénytelen csapat létszám.";
            public const string SourceEmailAlreadyRegistered = "Ezzel az e-mail címmel már regisztráltak egy csapatot!";

            public const string AssignmentAdded = "Sikeres jelentkezés!";
        }
    }
}
