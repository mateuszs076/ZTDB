using MongoDB.Driver;
using System.Collections.Generic;
using ZTDB.MongoDatabase.Models;

namespace ZTDB.MongoDatabase
{
    public class MongoContext
    {
        private readonly IMongoCollection<Flight> _flights;

        public MongoContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("flights");
            _flights = database.GetCollection<Flight>("flights");
        }

        /// <summary>
        /// Pobiera wszystkie loty
        /// </summary>
        /// <returns></returns>
        public List<Flight> Get() =>
          _flights.Find(a => true).ToList();

        /// <summary>
        /// Popiera określoną liczbę lotów
        /// </summary>
        /// <param name="limit">Ilość lotów do pobrania</param>
        /// <returns></returns>
        public List<Flight> Get(int limit) =>
          _flights.Find(a => true).Limit(limit).ToList();

        /// <summary>
        /// Popiera określony lot
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public Flight Get(string id) =>
            _flights.Find<Flight>(a => a.Id == id).FirstOrDefault();
    }
}