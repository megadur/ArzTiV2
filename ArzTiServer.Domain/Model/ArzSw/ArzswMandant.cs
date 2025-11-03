using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    public partial class ArzswMandant
    {
        public ArzswMandant()
        {
            ArzswBenutzers = new HashSet<ArzswBenutzer>();
            ArzswDatenbanks = new HashSet<ArzswDatenbank>();
        }

        public int ArzswMandantId { get; set; }
        /// <summary>
        /// Code-Kenner des Mandanten - muss eindeutig sein
        /// </summary>
        public string CodeKenner { get; set; } = null!;
        /// <summary>
        /// Allgemeiner name des Mandanten
        /// </summary>
        public string MandantName { get; set; } = null!;
        public string? Beschreibung { get; set; }

        public virtual ICollection<ArzswBenutzer> ArzswBenutzers { get; set; }
        public virtual ICollection<ArzswDatenbank> ArzswDatenbanks { get; set; }
    }
}
