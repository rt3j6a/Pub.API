using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Interface {
    public interface ITableManager {
        Task<IEnumerable<object>> GetAllTablesAsync();

        Task<IEnumerable<object>> GetFreeTablesAsync();

        Task<(bool success, string message)> DeleteTableAsync(int tableId);

        Task<(bool success, string message)> AddTableAsync(string tableName, decimal maxSeatNumber);

        Task<(bool success, string message)> AddTableReservationAsync(string teamName, string? comment, int tableId, int eventId);

        Task<object?> GetTableReservationAsync(int reservationId);

        Task<IEnumerable<object>> GetAllTableReservationsAsync();

        Task<(bool success, string message)> DeleteTableReservationAsync(int reservationId);

        Task<(bool success, string message)> UpdateTableReservationCommentAsync(int reservationId, string comment);

        Task<(bool success, string message)> DeleteAllTableReservationForEventAsync(int eventId);
    }
}
