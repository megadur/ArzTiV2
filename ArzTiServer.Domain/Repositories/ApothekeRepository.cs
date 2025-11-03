using Microsoft.EntityFrameworkCore;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Repositories
{
    public class ApothekeRepository : IApothekeRepository
    {
        private readonly ArzTiDbContext _context;

        public ApothekeRepository(ArzTiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ErApotheke>> GetAllAsync(int page, int pageSize)
        {
            return await _context.ErApothekes
                .OrderBy(a => a.ApothekeName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.ErApothekes.CountAsync();
        }

        public async Task<ErApotheke?> GetByIdAsync(int id)
        {
            return await _context.ErApothekes
                .FirstOrDefaultAsync(a => a.IdApotheke == id);
        }

        public async Task<ErApotheke?> GetByIkNrAsync(long apoIkNr)
        {
            return await _context.ErApothekes
                .FirstOrDefaultAsync(a => a.ApoIkNr == apoIkNr);
        }

        public async Task<ErApotheke> CreateAsync(ErApotheke apotheke)
        {
            apotheke.AenDatum = DateOnly.FromDateTime(DateTime.Now);
            apotheke.AenZeit = TimeOnly.FromDateTime(DateTime.Now);

            _context.ErApothekes.Add(apotheke);
            await _context.SaveChangesAsync();
            return apotheke;
        }

        public async Task<ErApotheke> UpdateAsync(ErApotheke apotheke)
        {
            apotheke.AenDatum = DateOnly.FromDateTime(DateTime.Now);
            apotheke.AenZeit = TimeOnly.FromDateTime(DateTime.Now);

            _context.ErApothekes.Update(apotheke);
            await _context.SaveChangesAsync();
            return apotheke;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var apotheke = await GetByIdAsync(id);
            if (apotheke == null)
                return false;

            _context.ErApothekes.Remove(apotheke);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ErApotheke>> SearchAsync(string searchTerm, int page, int pageSize)
        {
            var query = _context.ErApothekes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(a => 
                    a.ApothekeName.ToLower().Contains(searchTerm) ||
                    (a.ApothekeNameZusatz != null && a.ApothekeNameZusatz.ToLower().Contains(searchTerm)) ||
                    (a.InhaberVorname != null && a.InhaberVorname.ToLower().Contains(searchTerm)) ||
                    (a.InhaberNachname != null && a.InhaberNachname.ToLower().Contains(searchTerm)) ||
                    (a.Ort != null && a.Ort.ToLower().Contains(searchTerm)) ||
                    a.ApoIkNr.ToString().Contains(searchTerm));
            }

            return await query
                .OrderBy(a => a.ApothekeName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetSearchCountAsync(string searchTerm)
        {
            var query = _context.ErApothekes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(a => 
                    a.ApothekeName.ToLower().Contains(searchTerm) ||
                    (a.ApothekeNameZusatz != null && a.ApothekeNameZusatz.ToLower().Contains(searchTerm)) ||
                    (a.InhaberVorname != null && a.InhaberVorname.ToLower().Contains(searchTerm)) ||
                    (a.InhaberNachname != null && a.InhaberNachname.ToLower().Contains(searchTerm)) ||
                    (a.Ort != null && a.Ort.ToLower().Contains(searchTerm)) ||
                    a.ApoIkNr.ToString().Contains(searchTerm));
            }

            return await query.CountAsync();
        }

        public async Task<bool> ExistsByIkNrAsync(long apoIkNr, int? excludeId = null)
        {
            var query = _context.ErApothekes.Where(a => a.ApoIkNr == apoIkNr);
            
            if (excludeId.HasValue)
            {
                query = query.Where(a => a.IdApotheke != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}