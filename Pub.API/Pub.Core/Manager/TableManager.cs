using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pub.Core.Common;
using Pub.Core.Interface;
using Pub.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pub.Core.Manager {
    public class TableManager : CoreManager, ITableManager {
        public TableManager(IConfiguration configuration, IServiceScopeFactory scopeFactory) : base(configuration, scopeFactory) {
        }

        public async Task<(bool success, string message)> AddTableAsync(string tableName, decimal maxSeatNumber) {
            var tableExists=await provider.Tables.AnyAsync(x=>x.TableName.Equals(tableName));

            if (tableExists) {
                return (false, Messages.Table.TableAlreadyRegisteredWithGivenName);
            }

            if (!(maxSeatNumber > 0)) {
                return (false, Messages.Table.TableSeatNumberMustBeGreaterThanZero);
            }

            Table table = new Table { 
                TableName=tableName,
                MaxSeatNumber=decimal.ToInt32(maxSeatNumber)
            };

            try {
                await provider.Tables.AddAsync(table);
                await provider.SaveChangesAsync();
                return (true, Messages.Table.TableAddedSuccessfully);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        public async Task<(bool success, string message)> AddTableReservationAsync(string teamName, string? comment, int tableId, int eventId) {
            var teamHasReservation = await provider.TableReservations.AnyAsync(x => x.TeamName.Equals(teamName));

            if (teamHasReservation) {
                return (false, Messages.Table.TeamAlreadyHasReservation);
            }

            var tableReserved=await provider.TableReservations.AnyAsync(x=>x.TableId.Equals(tableId));

            if (tableReserved) {
                return (false, Messages.Table.TableAlreadyReserved);
            }

            TableReservation reservation = new TableReservation { 
                TeamName=teamName,
                Comment=comment,
                EventId=eventId,
                TableId=tableId
            };

            try {
                await provider.TableReservations.AddAsync(reservation);
                await provider.SaveChangesAsync();
                return (true, Messages.Table.TableReservedSuccessfully);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        public async Task<(bool success, string message)> DeleteAllTableReservationForEventAsync(int eventId) {
            var result=await provider.TableReservations.Where(x=>x.EventId.Equals(eventId)).ExecuteDeleteAsync();

            try {
                await provider.SaveChangesAsync();

                return result > 0 ? (true,Messages.Table.AllReservationDeletedSuccessfully) : (false,Messages.Table.NoReservationsToDelete);
            } catch (Exception) {
                return (false, Messages.InternalServerError);

            }
        }

        public async Task<(bool success, string message)> DeleteTableAsync(int tableId) {
            var result = await provider.Tables.Where(x => x.TableId == tableId).ExecuteDeleteAsync();

            try {
                await provider.SaveChangesAsync();
                return result == 1 ? (true, Messages.Table.TableDeletedSuccessfully) : (false, Messages.Table.TableDoesntExistsWithGivenId);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        public async Task<(bool success, string message)> DeleteTableReservationAsync(int reservationId) {
            var result=await provider.TableReservations.Where(x=>x.TableReservationId==reservationId).ExecuteDeleteAsync();

            try {
                await provider.SaveChangesAsync();
                return result == 1 ? (true, Messages.Table.TableReservationDeletedSuccessfully) : (false, Messages.Table.TableReservationDoesntExistsWithGivenId);
            } catch (Exception) {
                return (false, Messages.InternalServerError);
            }
        }

        public async Task<IEnumerable<object>> GetAllTableReservationsAsync() {
            return await provider.TableReservations.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllTablesAsync() {
            return await provider.Tables.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetFreeTablesAsync() {
            

            var tables = await provider.Tables.ToListAsync();
            var reservations=await provider.TableReservations.ToListAsync();

            List<Table> result = tables;

            if (tables.Count()==0 || reservations.Count()==0) {
                return Enumerable.Empty<object>();
            }

            foreach (var table in tables) {
                foreach(var reservation in reservations) {
                    if (table.TableId == reservation.TableId) {
                        result.Remove(table);
                    }
                }
            }

            return result;
                       
        }

        public async Task<object?> GetTableReservationAsync(int reservationId) {
            return await provider.TableReservations.FirstOrDefaultAsync(x=>x.TableReservationId == reservationId);
        }

        public async Task<(bool success, string message)> UpdateTableReservationCommentAsync(int reservationId, string comment) {
            var reservation = await provider.TableReservations.FirstOrDefaultAsync(x => x.TableReservationId == reservationId);

            if (reservation == null) {
                return (false, Messages.Table.TableReservationDoesntExistsWithGivenId);
            }

            reservation.Comment = comment;

            try {
                provider.Update(reservation);
                await provider.SaveChangesAsync();
                return (true, Messages.Table.TableReservationUpdatedSuccessfully);

            }catch(Exception) { 
                return (false,Messages.InternalServerError);
            }
        }
    }
}
