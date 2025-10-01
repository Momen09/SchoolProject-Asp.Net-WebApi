

using Microsoft.EntityFrameworkCore;
using SchoolPrj.Data.Entites.Identity;
using SchoolPrj.Infrastructure.Abstracts;
using SchoolPrj.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Data;

namespace SchoolPrj.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        public readonly DbSet<UserRefreshToken> _userRefreshTokens;
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userRefreshTokens = dbContext.Set<UserRefreshToken>();
        }
    }
}
