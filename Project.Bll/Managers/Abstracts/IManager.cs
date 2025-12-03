using Project.Bll.Dtos;
using Project.Bll.Results;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Managers.Abstracts
{
    public interface IManager<T, U> where T : class, IDto where U : BaseEntity
    {
        Task<Result<List<T>>> GetAllAsync();
        Task<Result<T>> GetByIdAsync(int id);
        Result<List<T>> GetActives();
        Result<List<T>> GetPassives();
        Result<List<T>> GetUpdateds();

        Task<Result> CreateAsync(T entity);
        Task<Result> UpdateAsync(T entity);
        Task<Result<string>> SoftDeleteAsync(int id);
        Task<Result<string>> HardDeleteAsync(int id);
    }
}