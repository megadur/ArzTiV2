using System;
using System.Collections.Generic;

namespace ArzTi3Server.Domain.Model.ArzSw
{
    public partial class ArzswOption
    {
        public int ArzswOptionId { get; set; }
        /// <summary>
        /// Zu welchen Typ gehört die Option - MANDANT | BENUTZER | DATENABNK
        /// </summary>
        public string OptionTyp { get; set; } = null!;
        /// <summary>
        /// Zugehörige ID zum option_typ
        /// </summary>
        public int OptionTypId { get; set; }
        public string Name { get; set; } = null!;
        /// <summary>
        /// Unterstützte Datentypen - INTEGER | STRING | BOOL
        /// </summary>
        public string Datentyp { get; set; } = null!;
        public string? Wert { get; set; }
        public string? Beschreibung { get; set; }
    }
}
