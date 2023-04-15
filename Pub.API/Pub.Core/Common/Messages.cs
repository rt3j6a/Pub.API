using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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

        public static class Table {
            public const string TableAlreadyReserved = "Az asztal már ki van osztva egy másik csapathoz!";
            public const string TableReservedSuccessfully = "Az asztal sikeresen kiosztva a csapathoz!";
            public const string TeamAlreadyHasReservation = "A csapat már rendelkezik foglalt asztallal!";
            public const string AllReservationDeletedSuccessfully = "Az esemény foglalásai sikeresen törölve!";
            public const string NoReservationsToDelete = "Az eseményhez nem tartozik asztal foglalás, amit törölni lehetne!";
            public const string TableDeletedSuccessfully = "Az asztal törlése sikeres volt!";
            public const string TableDoesntExistsWithGivenId = "Az asztal nem létezik, vagy már törölve lett!";
            public const string TableReservationDeletedSuccessfully = "Az asztal foglalás törlése sikeres volt!";
            public const string TableReservationDoesntExistsWithGivenId = "Az asztal foglalás nem létezik, vagy már törölve lett!";
            public const string TableReservationUpdatedSuccessfully = "A foglalás adatainak frissítése sikeres volt!";
            public const string TableAlreadyRegisteredWithGivenName = "Már létezik asztal a megadott névvel!";
            public const string TableAddedSuccessfully = "Asztal sikeresen regisztrálva!";
            public const string TableSeatNumberMustBeGreaterThanZero = "Az asztalhoz megadott ülőhelyek száma nem lehet 0 vagy kisebb";
        }

        public static class Post {
            public const string PostCreatedSuccessfully = "A poszt sikeresen létrehozva!";
            public const string PostDeletedSuccessfully = "A poszt törlése sikeres volt!";
            public const string PostDoesntExistsOrDeleted = "A posztot nem létezik, vagy már törölve lett!";
            public const string PostUpdatedSuccessfully = "A poszt sikeresen frissítve lett!";
        }

        public static class Result {
            public const string ResultForTeamAlreadyAssigned = "Ehhez a csapathoz már rögzítettek eredményt az eseménynél!";
            public const string ScoreMustBeGreaterThanZero = "A megadott pontszámnak nagyobbnak kell lennie nullánál!";
            public const string ResultAssignedSuccessfully = "Az eredmény rögzítése sikeres volt!";
            public const string ResultDeletedSuccessfully = "Az eredmény törlése sikeres volt!";
            public const string ResultDoestnExistOrAlreadyDeleted = "Az eredmény nem létezik, vagy már korábban törölve lett!";
            public const string ResultTeamScoreUpdatedSuccessfully = "A pontszám frissítése sikeres volt!";
        }

        public static class Picture {
            public const string LinkAlreadyCreatedForEvent = "Az eseményhez már hoztak létre egy albumot!";
            public const string EventLinkCreatedSuccessfully = "Az album sikeresen létrehozva az eseményhez!";
        }


        public static class Question {
            public const string QuestionAddedSuccessfully = "A kérdés sikeresen hozzáadva az eseményhez!";
            public const string QuestionDoesntExistsOrAlreadyRemoved = "A kérdés nem létezik, vagy már törölve lett!";
            public const string QuestionDeletedSuccessfully = "A kérdés sikeresen törölve!";
            public const string QuestionUpdatedSuccessfully = "A kérdés sikeresen frissítve!";
        }
    }
}
