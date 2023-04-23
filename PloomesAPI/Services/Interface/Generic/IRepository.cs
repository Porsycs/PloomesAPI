using PloomesAPI.Common;
using PloomesAPI.Model.Generic;

namespace PloomesAPI.Services.Interface.Generic
{
    public interface IRepository<T> where T : PloomesCommon
    {
        List<T> GetAll();
        T GetById(Guid Id);
        T Insert(T item);
        T Update(T item);
        void Delete(Guid Id);

    }
}
