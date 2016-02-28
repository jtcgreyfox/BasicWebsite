﻿using System.Threading.Tasks;
using MongoDB.Driver;
using Data.Interfaces;

namespace Data.Servers
{
    public class MongoDBServer : INoSQLDataProvider
    {
        private const string CONNECTION_STRING = "mongodb://Logs:8QPd_9iZRhBUu6i6cS.Ppd_LRmvmYcU8gUM0Oh5GeuE-@ds054128.mlab.com:54128/Logs";
        private const string DATABASE_NAME = "Logs";

        private MongoClient _client;
        private IMongoDatabase _database;

        public MongoDBServer()
        {
            _client = new MongoClient(CONNECTION_STRING + DATABASE_NAME);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        public async Task WriteDocument(string collectionName, dynamic document)
        {
            var collection = _database.GetCollection<dynamic>(collectionName);
            await collection.InsertOneAsync(document);
        }

        //public System.Collections.Generic.IEnumerable<string> GetCollectionIds(string collectionName)
        //{
        //    var collection = _client.GetDatabase(DatabaseName).GetCollection<dynamic>(collectionName);

        //    var map = new MongoDB.Bson.BsonJavaScript("function() { emit(this.cust_id, this.price); };");
        //    var reduce = new MongoDB.Bson.BsonJavaScript("");

        //    collection.MapReduce<dynamic>();
        //}
    }
}
