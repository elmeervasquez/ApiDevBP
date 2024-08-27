using ApiDevBP.Common;
using ApiDevBP.Entities;

namespace ApiDevBP.Services
{
    [Injectable]
    public class UserService : IUserService
    {
        public UserEntity Create(string name, string lastName)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserById()
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserByNameOrLastName(string name)
        {
            throw new NotImplementedException();
        }

        public List<UserEntity> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}