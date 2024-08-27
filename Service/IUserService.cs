
using ApiDevBP.Entities;

namespace ApiDevBP.Services
{
    public interface IUserService
    {
        UserEntity Create(string name, string lastName);

        void Delete(int id);

        void Update(int id);

        List<UserEntity> GetUsers();
        UserEntity GetUserById();
        UserEntity GetUserByNameOrLastName(string name);

    }
}