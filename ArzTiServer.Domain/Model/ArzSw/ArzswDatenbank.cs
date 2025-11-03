using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    public partial class ArzswDatenbank
    {
        public ArzswDatenbank()
        {
            ArzswBenutzers = new HashSet<ArzswBenutzer>();
        }

        public int ArzswDatenbankId { get; set; }
        public string DatenbankName { get; set; } = null!;
        /// <summary>
        /// Kompletter Connection-String für die Verbindung zur Datenbank
        /// </summary>
        public string DatenbankConnectionString { get; set; } = null!;
        /// <summary>
        /// Datenbank wird aktuell verwendet - z.B. kein Archiv, Backup, ...!
        /// </summary>
        public bool? DatenbankAktiv { get; set; }
        public int ArzswMandantId { get; set; }
        public string? Beschreibung { get; set; }

        public virtual ArzswMandant ArzswMandant { get; set; } = null!;
        public virtual ICollection<ArzswBenutzer> ArzswBenutzers { get; set; }
    }
}
