using VeniteckSolutions.Models;

namespace VeniteckSolutions.Services
{
    public interface IUserService
    {
        Task<bool> InsertUser(UserDTO user);
        Task<bool> UpdateUser(UserDTO user);
        Task<List<UserDTO>> GetAllUsers();

        // Retrieves a single user by their UserId
        Task<UserDTO?> GetUserById(int userId);
    }
}
