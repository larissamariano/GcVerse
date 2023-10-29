using GcVerse.Models.Category;
using GcVerse.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : class 
    {
        Task<int> CreateEntity(T entity);
        Task<bool> UpdateEntity(int entityId, T entity);
        Task<bool> DeleteEntity(int entityId);
        Task<T> GetEntityById(int entityId);
        Task<List<T>> GetEntities(int? queryId);
    }
}
