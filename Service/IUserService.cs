
using ApiDevBP.Entities;
using ApiDevBP.Models;

namespace ApiDevBP.Services
{
    public interface IUserService
    {
        UserEntity Create(UserEntity entity);

        void Delete(int id);

        UserEntity Update(int id, UserEntity entity);

        List<UserEntity> GetUsers();
        UserEntity GetUserById(int id);
    }
}