using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Infrastruce;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Services.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly MealOrderingDbContext context;
    public UserService(IMapper Mapper, MealOrderingDbContext Context)
    {
        mapper = Mapper;
        context = Context;
    }

    public async Task<UserDTO> GetUserById(Guid id)
    {
        return await context.Users
            .Where(i => i.Id == id)
            .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<List<UserDTO>> GetUsers()
    {
        return await context.Users
            .Where(i => (bool)i.IsActive)
            .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<UserDTO> CreateUser(UserDTO User)
    {
        var dbUser = await context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();

        if (dbUser != null)
            throw new Exception("ilgili kayıt zaten mevcut");

        dbUser = mapper.Map<Data.Models.Users>(User);

        await context.Users.AddAsync(dbUser);
        int result = await context.SaveChangesAsync();
        return mapper.Map<UserDTO>(dbUser);

    }
    public async Task<UserDTO> UpdateUser(UserDTO User)
    {
        var dbUser = await context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();

        if (dbUser == null)
            throw new Exception("ilgili kayıt  bulunamadı ");

        //dbUser = mapper.Map<Data.Models.Users>(User);
        mapper.Map(User, dbUser);
        int result = await context.SaveChangesAsync();
        return mapper.Map<UserDTO>(dbUser);
    }

    public async Task<bool> DeleteUserById(Guid id)
    {
        var dbUser = await context.Users.Where(i => i.Id == id).FirstOrDefaultAsync();

        if (dbUser == null)
            throw new Exception("Kullanıcı bulunamadı");

        context.Users.Remove(dbUser);
        int result = await context.SaveChangesAsync();
        return result > 0;

    }
}