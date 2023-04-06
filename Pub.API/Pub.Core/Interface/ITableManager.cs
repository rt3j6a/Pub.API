using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface ITableManager {
        Task<IEnumerable<object>> GetAllTables();

        Task<IEnumerable<object>> GetFreeTables();

        Task<(bool success, string message)> DeleteTable(int tableId);

        Task<(bool success, string message)> AddTableReservation(int teamName, string? comment, int tableId, int eventId);

        Task<object?> GetTableReservation(int reservationId);

        Task<(bool success, string message)> DeleteTableReservation(int reservationId);

        Task<(bool success, string message)> UpdateTableReservationComment(int reservationId, string comment);

        Task<(bool success, string message)> DeleteAllTableReservationForEvent();
    }
}
