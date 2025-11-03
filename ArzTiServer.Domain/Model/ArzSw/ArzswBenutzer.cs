using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ArzSw;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    public partial class ArzswBenutzer
    {
        public int ArzswBenutzerId { get; set; }
        /// <summary>
        /// Allgemeiner Name des Benutzers
        /// </summary>
        public string BenutzerName { get; set; } = null!;
        /// <summary>
        /// Login Name des Benutzers
        /// </summary>
        public string LoginName { get; set; } = null!;
        public string LoginPasswort { get; set; } = null!;
        public string? LoginPasswortCrypt { get; set; }
        public string? Beschreibung { get; set; }
        public int ArzswMandantId { get; set; }
        public int ArzswDatenbankId { get; set; }

        public virtual ArzswDatenbank ArzswDatenbank { get; set; } = null!;
        public virtual ArzswMandant ArzswMandant { get; set; } = null!;
    }
}
