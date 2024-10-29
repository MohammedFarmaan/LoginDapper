using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<string?> CreateUser(User user);
        Task<string?> UpdateUser(User user, int id);
        Task<string> DeleteUser(int id);
        Task<bool> LoginUser(string email, string password);
    }
}