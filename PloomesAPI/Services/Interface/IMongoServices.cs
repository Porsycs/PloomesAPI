using MongoDB.Driver;
using PloomesAPI.Model.ViewModel;

namespace PloomesAPI.Services.Interface
{
    public interface IMongoServices
    {
        object Create(MongoLogsViewModel.Log log, object document);
    }
}
