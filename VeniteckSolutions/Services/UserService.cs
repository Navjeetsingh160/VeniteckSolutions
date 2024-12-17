using Microsoft.Data.SqlClient;
using VeniteckSolutions.Models;

namespace VeniteckSolutions.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly string _connectionString;

        // Constructor to initialize _connectionString
        public UserService(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Method to insert a user
        public async Task<bool> InsertUser(UserDTO user)
        {
            await ExecuteStoredProcedureAsync("InsertUser", (command) =>
            {
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@Source", user.Source ?? (object)DBNull.Value);
            });
            return true;
        }

        // Method to update a user
        public async Task<bool> UpdateUser(UserDTO user)
        {
            await ExecuteStoredProcedureAsync("UpdateUser", (command) =>
            {
                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Address", user.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@Source", user.Source ?? (object)DBNull.Value);
            });
            return true;
        }

        // Method to retrieve all users
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = new List<UserDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync(); // Open the connection

                    using (var command = new SqlCommand("GetAllUser", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                users.Add(new UserDTO
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Address")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    MobileNumber = reader.GetString(reader.GetOrdinal("MobileNumber")),
                                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                    Source = reader.IsDBNull(reader.GetOrdinal("Source"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Source"))
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or rethrow as needed
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }

            return users;
        }

        // Method to retrieve a single user by Id
        public async Task<UserDTO?> GetUserById(int userId)
        {
            UserDTO? user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync(); // Open the connection

                    using (var command = new SqlCommand("GetUserById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                user = new UserDTO
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Address")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    MobileNumber = reader.GetString(reader.GetOrdinal("MobileNumber")),
                                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                    Source = reader.IsDBNull(reader.GetOrdinal("Source"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Source"))
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or rethrow as needed
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }

            return user;
        }
    }
}
