using System.Collections.Generic;
using System.Threading.Tasks;
using ArzTi3Server.Domain.DTOs;
using ArzTi3Server.Domain.DTOs.V2;

namespace ArzTi3Server.Domain.Repositories
{
    public interface IPrescriptionRepository
    {
        /// <summary>
        /// Fetches all new prescriptions (all types) with XmlRequest data where TransferArz=false, paginated.
        /// </summary>
        Task<IEnumerable<PrescriptionDto>> GetNewPrescriptionsAsync(int page, int pageSize, string? rezeptType = null);

        /// <summary>
        /// Gets the total count of new prescriptions for pagination.
        /// </summary>
        Task<int> GetNewPrescriptionsCountAsync(string? rezeptType = null);

        /// <summary>
        /// Fetches specifically E-Rezept XML requests where TransferArz=false.
        /// </summary>
        Task<IEnumerable<PrescriptionDto>> GetNewErezeptXmlRequestsAsync(int page, int pageSize);

        /// <summary>
        /// Transfers prescription data for ensurance mediation.
        /// </summary>
        Task TransferPrescriptionsAsync(IEnumerable<PrescriptionDto> prescriptions);

        /// <summary>
        /// Marks prescriptions as read by setting TransferArz=true.
        /// </summary>
        Task MarkAsReadAsync(IEnumerable<PrescriptionDto> prescriptions);

        /// <summary>
        /// Marks prescriptions as read by setting TransferArz=true using UUIDs.
        /// </summary>
        Task MarkAsReadByUuidAsync(IEnumerable<PrescriptionDto> prescriptions);

        /// <summary>
        /// Sets prescription status to ABGERECHNET after calculation.
        /// </summary>
        Task SetStatusAbgerechnetAsync(IEnumerable<PrescriptionDto> prescriptions);

        /// <summary>
        /// Sets prescription status to ABGERECHNET after calculation using UUIDs.
        /// </summary>
        Task SetStatusAbgerechnetByUuidAsync(IEnumerable<PrescriptionDto> prescriptions);

        // ARZ_TI 2.0 Enhanced Methods
        /// <summary>
        /// ARZ_TI 2.0: High-performance retrieval of all new prescriptions with enhanced metadata
        /// </summary>
        Task<IEnumerable<PrescriptionV2Dto>> GetNewPrescriptionsV2Async(int page, int pageSize, string? type = null);

        /// <summary>
        /// ARZ_TI 2.0: Count new prescriptions for V2 API pagination
        /// </summary>
        Task<int> GetNewPrescriptionsCountV2Async(string? type = null);

        /// <summary>
        /// ARZ_TI 2.0: Pharmacy-specific prescription retrieval with error information
        /// </summary>
        Task<IEnumerable<PrescriptionV2Dto>> GetPharmacyPrescriptionsV2Async(string pharmacyId, int page, int pageSize, string? type = null);

        /// <summary>
        /// ARZ_TI 2.0: Count pharmacy prescriptions for V2 API pagination
        /// </summary>
        Task<int> GetPharmacyPrescriptionsCountV2Async(string pharmacyId, string? type = null);

        /// <summary>
        /// ARZ_TI 2.0: Bulk status retrieval with comprehensive error reporting
        /// </summary>
        Task<IEnumerable<PrescriptionV2Dto>> GetBulkPrescriptionStatusV2Async(IEnumerable<string> uuids);

        /// <summary>
        /// ARZ_TI 2.0: Bulk status updates with transaction management
        /// </summary>
        Task<BulkUpdateResult> BulkUpdatePrescriptionStatusV2Async(IEnumerable<PrescriptionStatusUpdate> updates);
    }
}
