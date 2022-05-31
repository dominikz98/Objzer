using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    internal static class CTHistoryExtensions
    {
        public static async Task<List<CTHistory>> GetByEntity(this IQueryable<CTHistory> query, Guid id)
            => await query.AsNoTracking()
                .Where(x => x.EntityId == id)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
    }
}
