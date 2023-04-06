using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Common {
    public static class Messages {

        public const string InternalServerError = "Szerver oldali hiba történt! Vegye fel a kapcsolatot a fejlesztővel.";
        public static class Authentication {
            public const string AccountAlreadyExistsWithGivenCredentials = "A fiók már létezik a megadott e-mail címmel/fióknévvel";
            public const string AccountCreatedSuccessfully = "Fiók sikeresen létrehozva!";
            public const string LoggedInSuccessfully = "Sikeres bejelentkezés!";
            public const string AccountDoesntExistsWithGivenCredentials = "Nem létezik fiók a megadott adatokkal!";
        }
        public static class Team {
            public const string TeamAlreadyRegistered = "Ilyen csapat már regisztrálva van ezzel a névvel!";
            public const string TeamMemberCountInvalid = "Érvénytelen csapat létszám.";
            public const string SourceEmailAlreadyRegistered = "Ezzel az e-mail címmel már regisztráltak egy csapatot!";
            public const string TeamAssignmentDoestnExistsOrDeleted = "A jelentkezés nem létezik, vagy már törölve lett!";
            public const string AssignmentAdded = "Sikeres jelentkezés!";
            public const string AssignmentStatusUpdatedSuccessfully = "A jelentkezés státusza sikeresen frissítve lett!";
        }

        public static class Event {
            public const string EventAlreadyExistsWithGivenDetails = "Már létrehoztak egy eseményt ezzel a névvel.";
            public const string EventAddedSuccessfully = "Esemény létrehozása sikeres volt!";
            public const string EventDoesntExistsOrAlreadyRemoved = "Az esemény nem létezik, vagy már törölve van.";
            public const string EventUpdatedSuccessfully = "Az esemény státusza sikeresen frissítve lett!";
        }
    }
}
