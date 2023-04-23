using Microsoft.EntityFrameworkCore;
using PloomesAPI.Common;
using PloomesAPI.Model.Context;
using PloomesAPI.Model.Generic;
using PloomesAPI.Services.Interface.Generic;

namespace PloomesAPI.Services.Repository.Generic
{
	public class Repository<T> : IRepository<T> where T : PloomesCommon
    {
        private readonly BancoContext _dbContext;
        private DbSet<T> _dbSet;
        public Repository(BancoContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public List<T> GetAll()
        {
            var dado = _dbSet.AsNoTracking().ToList();
            return dado;
        }

        public T GetById(Guid Id)
        {
            var dado = _dbSet.AsNoTracking().FirstOrDefault(f => f.Id == Id);
            return dado;
        }

        public T Insert(T item)
        {
            try
            {
                _dbContext.Add(item);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return item;
        }

        public T Update(T item)
        {

            var result = GetById(item.Id);

            if (result != null)
            {
                try
                {
                    _dbContext.Entry(result).CurrentValues.SetValues(item);
                    _dbContext.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
                return null;
        }

        public void Delete(Guid Id)
        {

            var result = GetById(Id);
            if (result != null) 
            {
                try
                {
                    _dbSet.Remove(result);
                    _dbContext.SaveChanges();
                }
				catch (Exception ex)
				{
					throw;
				}
			}
        }
        public bool Existe(Guid Id)
        {
            var existe = _dbSet.AsNoTracking().Any(f => f.Id == Id);
            return existe;
        }
    }
}
