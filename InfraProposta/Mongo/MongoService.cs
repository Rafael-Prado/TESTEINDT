using DomainProposta.Entities;
using DomainProposta.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InfraProposta.Mongo
{
    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<BsonDocument> _orderItemsCollection;

        public MongoService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _orderItemsCollection = database.GetCollection<BsonDocument>("HistoricoProposta");
        }

        public async Task SaveHistoricoPropostaAsync(PropostaMongo proposta)
        {
            var bson = new BsonDocument();

            if (proposta != null)
            {
                bson = proposta.ToBsonDocument();
                await _orderItemsCollection.InsertOneAsync(bson);
            }
                
        }
    }
}

