using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PloomesAPI.Model.ViewModel;
using PloomesAPI.Services.Interface;

namespace PloomesAPI.Services
{
    public class MongoServices : IMongoServices
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        public MongoServices(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new MongoClient(_configuration.GetSection("MongoDB:ConnectionString").Value);
            database = client.GetDatabase(_configuration.GetSection("MongoDB:DatabaseName").Value);
        }

        private IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }

        public object Create(MongoLogsViewModel.Log log, object document)
        {
            var collection = GetCollection<object>(log.ToString());
            collection.InsertOne(document);
            return document;
        }
    }
}
