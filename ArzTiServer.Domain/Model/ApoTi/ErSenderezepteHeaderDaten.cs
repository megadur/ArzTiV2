using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteHeaderDaten
    {
        public int IdSenderezepteHeaderDaten { get; set; }
        public int? IdSenderezepteHeader { get; set; }
        /// <summary>
        /// Eindeutige Header - Universal Unique Identifier (UUID)
        /// </summary>
        public string HeaderUuid { get; set; } = null!;
        /// <summary>
        /// Bestätigung vom ARZ - Transfer der Rezeptdaten erfolgt
        /// </summary>
        public bool? TransferArz { get; set; }
        /// <summary>
        /// Kompletter Original XML-Request der Apotheke - zur Weiterleitung ins ARZ
        /// </summary>
        public string XmlRequest { get; set; } = null!;

        public virtual ErSenderezepteHeader? IdSenderezepteHeaderNavigation { get; set; }
    }
}
