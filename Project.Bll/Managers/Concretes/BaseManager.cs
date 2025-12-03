using AutoMapper;
using Project.Bll.Delegates;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Results;
using Project.Dal.Repositories.Abstracts;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Managers.Concretes
{
    public abstract class BaseManager<T, U> : IManager<T, U> where T : class, IDto where U : BaseEntity
    {
        private readonly IRepository<U> _repository;
        protected readonly IMapper _mapper;

        // Delegate'ler
        protected ErrorHandlerDelegate ErrorHandler;
        protected LogDelegate Logger;

        protected BaseManager(IRepository<U> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            // Varsayılan error handler
            ErrorHandler = DefaultErrorHandler;

            // Varsayılan logger
            Logger = DefaultLogger;
        }

        // Varsayılan hata yönetimi - Sadece mesaj döndürür
        private string DefaultErrorHandler(Exception ex, string operationName)
        {
            Logger($"HATA - {operationName}: {ex.Message}", "Error");
            return $"{operationName} sırasında bir hata oluştu: {ex.Message}";
        }

        // Varsayılan loglama
        private void DefaultLogger(string message, string logLevel = "Info")
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] {message}");
        }

        // CREATE işlemi - Delegate ile hata yönetimi
        public async Task<Result> CreateAsync(T entity)
        {
            try
            {
                Logger($"Create işlemi başlatıldı - Entity: {typeof(T).Name}");

                U domainEntity = _mapper.Map<U>(entity);
                domainEntity.CreatedDate = DateTime.Now;
                domainEntity.Status = Entities.Enums.DataStatus.Inserted;

                await _repository.CreateAsync(domainEntity);

                Logger($"Create işlemi başarılı - Entity: {typeof(T).Name}, Id: {domainEntity.Id}");
                return Result.Success("Veri başarıyla eklendi");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "CreateAsync");
                return Result.Failure(errorMessage);
            }
        }

        // UPDATE işlemi - Delegate ile hata yönetimi
        public async Task<Result> UpdateAsync(T entity)
        {
            try
            {
                Logger($"Update işlemi başlatıldı - Entity: {typeof(T).Name}, Id: {entity.Id}");

                U originalValue = await _repository.GetByIdAsync(entity.Id);

                if (originalValue == null)
                {
                    Logger($"Update başarısız - Veri bulunamadı. Id: {entity.Id}", "Warning");
                    return Result.Failure("Güncellenecek veri bulunamadı");
                }

                U newValue = _mapper.Map<U>(entity);
                newValue.UpdatedDate = DateTime.Now;
                newValue.Status = Entities.Enums.DataStatus.Updated;

                await _repository.UpdateAsync(originalValue, newValue);

                Logger($"Update işlemi başarılı - Entity: {typeof(T).Name}, Id: {entity.Id}");
                return Result.Success("Veri başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "UpdateAsync");
                return Result.Failure(errorMessage);
            }
        }

        // SOFT DELETE işlemi - Delegate ile hata yönetimi
        public async Task<Result<string>> SoftDeleteAsync(int id)
        {
            try
            {
                Logger($"SoftDelete işlemi başlatıldı - Entity: {typeof(T).Name}, Id: {id}");

                U originalValue = await _repository.GetByIdAsync(id);

                if (originalValue == null || originalValue.Status == Entities.Enums.DataStatus.Deleted)
                {
                    Logger($"SoftDelete başarısız - Veri zaten pasif veya bulunamadı. Id: {id}", "Warning");
                    return Result<string>.Failure("Veri ya zaten pasif ya da bulunamadı");
                }

                originalValue.Status = Entities.Enums.DataStatus.Deleted;
                originalValue.DeletedDate = DateTime.Now;
                await _repository.SaveChangesAsync();

                Logger($"SoftDelete işlemi başarılı - Entity: {typeof(T).Name}, Id: {id}");
                return Result<string>.Success($"{id} id'li veri pasife çekilmiştir", "Pasifleştirme başarılı");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "SoftDeleteAsync");
                return Result<string>.Failure(errorMessage);
            }
        }

        // HARD DELETE işlemi - Delegate ile hata yönetimi
        public async Task<Result<string>> HardDeleteAsync(int id)
        {
            try
            {
                Logger($"HardDelete işlemi başlatıldı - Entity: {typeof(T).Name}, Id: {id}");

                U originalValue = await _repository.GetByIdAsync(id);

                if (originalValue == null || originalValue.Status != Entities.Enums.DataStatus.Deleted)
                {
                    Logger($"HardDelete başarısız - Sadece pasif veriler silinebilir. Id: {id}", "Warning");
                    return Result<string>.Failure("Sadece bulunabilen ve pasif veriler silinebilir");
                }

                await _repository.DeleteAsync(originalValue);

                Logger($"HardDelete işlemi başarılı - Entity: {typeof(T).Name}, Id: {id}");
                return Result<string>.Success($"{id} id'li veri silinmiştir", "Silme başarılı");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "HardDeleteAsync");
                return Result<string>.Failure(errorMessage);
            }
        }

        // QUERY işlemleri
        public async Task<Result<List<T>>> GetAllAsync()
        {
            try
            {
                Logger($"GetAll işlemi başlatıldı - Entity: {typeof(T).Name}");

                List<U> values = await _repository.GetAllAsync();
                var mappedValues = _mapper.Map<List<T>>(values);

                Logger($"GetAll işlemi başarılı - {mappedValues.Count} kayıt bulundu");
                return Result<List<T>>.Success(mappedValues, "Veriler başarıyla getirildi");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "GetAllAsync");
                return Result<List<T>>.Failure(errorMessage);
            }
        }

        public async Task<Result<T>> GetByIdAsync(int id)
        {
            try
            {
                Logger($"GetById işlemi başlatıldı - Entity: {typeof(T).Name}, Id: {id}");

                U value = await _repository.GetByIdAsync(id);

                if (value == null)
                {
                    Logger($"GetById başarısız - Veri bulunamadı. Id: {id}", "Warning");
                    return Result<T>.Failure("Veri bulunamadı");
                }

                var mappedValue = _mapper.Map<T>(value);

                Logger($"GetById işlemi başarılı - Entity: {typeof(T).Name}, Id: {id}");
                return Result<T>.Success(mappedValue, "Veri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "GetByIdAsync");
                return Result<T>.Failure(errorMessage);
            }
        }

        public Result<List<T>> GetActives()
        {
            try
            {
                Logger($"GetActives işlemi başlatıldı - Entity: {typeof(T).Name}");

                List<U> values = _repository.Where(x => x.Status != Entities.Enums.DataStatus.Deleted).ToList();
                var mappedValues = _mapper.Map<List<T>>(values);

                Logger($"GetActives işlemi başarılı - {mappedValues.Count} aktif kayıt bulundu");
                return Result<List<T>>.Success(mappedValues, "Aktif veriler getirildi");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "GetActives");
                return Result<List<T>>.Failure(errorMessage);
            }
        }

        public Result<List<T>> GetPassives()
        {
            try
            {
                Logger($"GetPassives işlemi başlatıldı - Entity: {typeof(T).Name}");

                List<U> values = _repository.Where(x => x.Status == Entities.Enums.DataStatus.Deleted).ToList();
                var mappedValues = _mapper.Map<List<T>>(values);

                Logger($"GetPassives işlemi başarılı - {mappedValues.Count} pasif kayıt bulundu");
                return Result<List<T>>.Success(mappedValues, "Pasif veriler getirildi");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "GetPassives");
                return Result<List<T>>.Failure(errorMessage);
            }
        }

        public Result<List<T>> GetUpdateds()
        {
            try
            {
                Logger($"GetUpdateds işlemi başlatıldı - Entity: {typeof(T).Name}");

                List<U> values = _repository.Where(x => x.Status == Entities.Enums.DataStatus.Updated).ToList();
                var mappedValues = _mapper.Map<List<T>>(values);

                Logger($"GetUpdateds işlemi başarılı - {mappedValues.Count} güncellenmiş kayıt bulundu");
                return Result<List<T>>.Success(mappedValues, "Güncellenmiş veriler getirildi");
            }
            catch (Exception ex)
            {
                string errorMessage = ErrorHandler(ex, "GetUpdateds");
                return Result<List<T>>.Failure(errorMessage);
            }
        }
    }
}