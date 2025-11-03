using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteErezept
    {
        public ErSenderezepteErezept()
        {
            ErSenderezepteErezeptDatens = new HashSet<ErSenderezepteErezeptDaten>();
            ErSenderezepteErezeptStatuses = new HashSet<ErSenderezepteErezeptStatus>();
            ErSenderezepteErezeptStatusinfos = new HashSet<ErSenderezepteErezeptStatusinfo>();
        }

        public int IdSenderezepteErezept { get; set; }
        public int IdSenderezepteHeader { get; set; }
        public string RzLieferId { get; set; } = null!;
        public string? RzDatum { get; set; }
        public DateOnly? RzLieferDatum { get; set; }
        public TimeOnly? RzLieferZeit { get; set; }
        public string? AvsId { get; set; }
        public string? AbrechnungsPeriode { get; set; }
        public long? ApoIkNr { get; set; }
        public string? ErezeptId { get; set; }
        /// <summary>
        /// Base64 Codierung der eRezept Daten
        /// </summary>
        public string? ErezeptData { get; set; }
        /// <summary>
        /// FHIR-Bundle der eVerordnung
        /// </summary>
        public string? ErezeptEverordnungData { get; set; }
        /// <summary>
        /// FHIR-Bundle der Quittung
        /// </summary>
        public string? ErezeptQuittungData { get; set; }
        /// <summary>
        /// FHIR-Bundle der eAbgabe
        /// </summary>
        public string? ErezeptEabgabeData { get; set; }

        public virtual ErSenderezepteHeader IdSenderezepteHeaderNavigation { get; set; } = null!;
        public virtual ICollection<ErSenderezepteErezeptDaten> ErSenderezepteErezeptDatens { get; set; }
        public virtual ICollection<ErSenderezepteErezeptStatus> ErSenderezepteErezeptStatuses { get; set; }
        public virtual ICollection<ErSenderezepteErezeptStatusinfo> ErSenderezepteErezeptStatusinfos { get; set; }
    }
}
