using MealOrdering.Shared.DTO;

namespace MealOrdering.Server.Services.Infrastruce;

public interface IUserService
{
    public Task<UserDTO> GetUserById(Guid id);
    public Task<List<UserDTO>> GetUsers();
    public Task<UserDTO> CreateUser(UserDTO User);
    public Task<UserDTO> UpdateUser(UserDTO User);
    public Task<bool> DeleteUserById(Guid id);
}