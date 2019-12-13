using MongoDB.Driver;
using System;

namespace ZTDB.MongoDatabase
{
    public class MongoContext
    {
        public MongoClient Client { get; set; }
        public MongoContext()
        {
            Client = new MongoClient();
        }
    }
}
