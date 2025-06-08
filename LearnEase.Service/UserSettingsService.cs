using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;
using System;
using System.Collections.Generic;
namespace LearnEase.Service
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<UserSettings> _repo;

        public UserSettingsService(IUnitOfWork uow)
        {
            _uow = uow;
            _repo = _uow.GetRepository<UserSettings>();
        }

        public async Task<IEnumerable<UserSettings>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<UserSettings?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<UserSettings> AddAsync(UserSettings settings)
        {
            await _repo.AddAsync(settings);
            await _uow.SaveAsync();
            return settings;
        }

        public async Task<UserSettings> UpdateAsync(UserSettings settings)
        {
            _repo.Update(settings);
            await _uow.SaveAsync();
            return settings;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var settings = await _repo.GetByIdAsync(id);
            if (settings == null) return false;
            _repo.Delete(settings);
            await _uow.SaveAsync();
            return true;
        }
    }

}
