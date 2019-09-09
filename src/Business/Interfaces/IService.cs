using System;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Interfaces
{
    public interface IService<TEntity> where TEntity : Entity
    {
        Task Add(TEntity obj);
        Task Update(TEntity obj);
        Task Delete(Guid id);
    }
}
