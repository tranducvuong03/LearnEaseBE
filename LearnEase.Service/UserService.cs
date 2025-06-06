using LearnEase.Repository;
using LearnEase.Repository.EntityModel;
using LearnEase.Service.IServices;

namespace LearnEase.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _repo;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.GetRepository<User>();
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<User?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task<User> AddAsync(User user)
        {
            await _repo.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _repo.Update(user);
            await _unitOfWork.SaveAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }

}
