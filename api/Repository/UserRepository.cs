using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Services;
using Dapper;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDBContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(DapperDBContext dapperDBContext, IPasswordHasher passwordHasher)
        {
            _context = dapperDBContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<string?> CreateUser(User user)
        {
            var query = "INSERT INTO users VALUES(@id, @username, @email, @password)";
            var existUserQuery = "SELECT * FROM users WHERE id=@id";
            using var connection = _context.CreateConnection();   

            var existingUser = await connection.QueryFirstOrDefaultAsync(existUserQuery, new {user.Id});
            
            if(existingUser != null){
                return null;

            }else{
                var passwordHasher = new PasswordHasher();
                var password = passwordHasher.Hash(user.Password);
                var newUser = await connection.ExecuteAsync(query, new {user.Id, user.Username, user.Email, password});

                return "User created successfully";
            }
        }

        public async Task<string> DeleteUser(int id)
        {
            var query = "Delete FROM users WHERE id=@id";
            using var connection = _context.CreateConnection();

            var user = await connection.ExecuteAsync(query, new { id });

            return "deleted user successfully";
        }

        public async Task<List<User>> GetAllUsers()
        {
            var query = "SELECT * FROM users";
            using var connection = _context.CreateConnection();
            var users = await connection.QueryAsync<User>(query);

            return users.ToList();
        }

        public async Task<User> GetUserById(int id)
        {
            var query = "SELECT * FROM users WHERE id=@id";
            using var connection = _context.CreateConnection();
            var user = await connection.QueryFirstAsync<User>(query, new { id });

            return user;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            // var query ="SELECT * FROM users WHERE email=@email AND password=@password";
            using var connection = _context.CreateConnection();

            // get user pass from db to verify with current password
            var getUserQuery = "SELECT * from users where email=@email";
            var user = await connection.QueryFirstOrDefaultAsync(getUserQuery, new { email });
            
            // Verifying password hash
            // var passwordHasher = new PasswordHasher();
            var isPassordValid = _passwordHasher.Verify(password, user.password);

            // var isValidUser = await connection.QueryFirstOrDefaultAsync(query, new { email, isPassordValid});

            if(isPassordValid == true){
                return true;
            }
            return false;
        }

        public async Task<string?> UpdateUser(User user, int id)
        {
            var query = "UPDATE users SET username=@username, email=@email, password=@password where id=@id";
            var existUserQuery = "SELECT * FROM users WHERE id=@id";
            using var connection = _context.CreateConnection();   

            var existingUser = await connection.QueryFirstOrDefaultAsync(existUserQuery, new {id});
            
            if(existingUser == null){
                return null;
            }else{
                // var passwordHasher = new PasswordHasher();
                var password = _passwordHasher.Hash(user.Password);
                var updatedUser = await connection.ExecuteAsync(query, new {id, user.Username, user.Email, password});

                return "User updated successfully";
            }
        }
    }
}