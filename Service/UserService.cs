using ApiDevBP.Common;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Persistence;

namespace ApiDevBP.Services
{
    [Injectable]
    public class UserService : IUserService
    {
        private readonly BaseRepository _userRepository;

        public UserService(BaseRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserEntity> GetUsers()
        {
            return _userRepository.GetAll<UserEntity>();
        }

        public UserEntity Create(UserEntity entity)
        {
            Validate(entity.Name, entity.Lastname);
            return _userRepository.Create(entity);
        }

        public void Delete(int id)
        {
            var entityToDelete = _userRepository.GetById<UserEntity>(id);
            if (entityToDelete == null)
            {
                throw new Exception("User not found");
            }
            _userRepository.Delete(entityToDelete);
        }

        public UserEntity? Update(int id, UserEntity model)
        {
            Validate(model.Name, model.Lastname);
            var entityToUpdate = _userRepository.GetById<UserEntity>(id);
            if (entityToUpdate == null)
            {
                throw new Exception("User not found");
            }
            entityToUpdate.Update(model.Name, model.Lastname);
            return _userRepository.Update(entityToUpdate);
        }

        public UserEntity GetUserById(int id)
        {
            return _userRepository.GetById<UserEntity>(id);
        }

        static void Validate(string name, string lastName)
        {
            if (int.TryParse(name, out int _) || int.TryParse(lastName, out int _))
            {
                throw new Exception("Name or lastName can't be a  number");
            }
        }
    }
}