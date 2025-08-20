using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;
using MinimalApi.Infra.Db;
using MinimalApi.Infra.Interfaces;

namespace MinimalApi.Domain.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly DbContexto _ctx;
        public AdministratorService(DbContexto context)
        {
            _ctx = context;
        }

        async public Task<Administrator?> Login(LoginDTO loginDTO)
        {
            var matchedLogins = await _ctx.Administrators.Where(login => login.Email == loginDTO.Email && login.Password == loginDTO.Senha).FirstOrDefaultAsync();
            return matchedLogins;
        }

        public async Task<Administrator?> Register(RegisterDTO registerDTO)
        {
            Administrator administrator = new Administrator
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                Profile = Enums.Profile.Editor
            };

            var entity = await _ctx.Administrators.AddAsync(administrator);
            await _ctx.SaveChangesAsync();

            return entity.Entity;
        }
        public async Task<List<Administrator>> All(int? page, int? pageSize)
        {
            var query = _ctx.Administrators.AsQueryable();

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Administrator?> FindById(int id)
        {
            return await _ctx.Administrators.FindAsync(id);
        }
    }
}
