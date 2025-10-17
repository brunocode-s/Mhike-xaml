using MHikePrototype.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MHikePrototype.Data
{
    public class DbService
    {
        private readonly SQLiteAsyncConnection db;

        public DbService(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Hike>().Wait();
            db.CreateTableAsync<Observation>().Wait();
        }

        // Hike
        public Task<int> InsertHike(Hike h) => db.InsertAsync(h);
        public Task<int> UpdateHike(Hike h) => db.UpdateAsync(h);
        public Task<int> DeleteHike(Hike h) => db.DeleteAsync(h);
        public Task<List<Hike>> GetAllHikes() => db.Table<Hike>().OrderBy(x => x.DateIso).ToListAsync();
        public Task<Hike> GetHike(int id) => db.Table<Hike>().Where(x => x.Id == id).FirstOrDefaultAsync();

        // Observation
        public Task<int> InsertObs(Observation o) => db.InsertAsync(o);
        public Task<int> UpdateObs(Observation o) => db.UpdateAsync(o);
        public Task<int> DeleteObs(Observation o) => db.DeleteAsync(o);
        public Task<List<Observation>> GetObsForHike(int hikeId)
            => db.Table<Observation>().Where(x => x.HikeId == hikeId).OrderByDescending(x => x.ObservedAtIso).ToListAsync();

        // Reset
        public async Task ResetDb()
        {
            await db.DeleteAllAsync<Observation>();
            await db.DeleteAllAsync<Hike>();
        }
    }
}
