using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Repositories
{
    public interface IApothekeRepository
    {
        Task<IEnumerable<ErApotheke>> GetAllAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<ErApotheke?> GetByIdAsync(int id);
        Task<ErApotheke?> GetByIkNrAsync(long apoIkNr);
        Task<ErApotheke> CreateAsync(ErApotheke apotheke);
        Task<ErApotheke> UpdateAsync(ErApotheke apotheke);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ErApotheke>> SearchAsync(string searchTerm, int page, int pageSize);
        Task<int> GetSearchCountAsync(string searchTerm);
        Task<bool> ExistsByIkNrAsync(long apoIkNr, int? excludeId = null);
    }
}