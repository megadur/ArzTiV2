using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezepteEmuster16Daten
    {
        public int IdSenderezepteEmuster16Daten { get; set; }
        public int? IdSenderezepteEmuster16 { get; set; }
        /// <summary>
        /// Eindeutiger Rezept - Universal Unique Identifier (UUID)
        /// </summary>
        public string RezeptUuid { get; set; } = null!;
        /// <summary>
        /// Bestätigung vom ARZ - Transfer der Rezeptdaten erfolgt
        /// </summary>
        public bool? TransferArz { get; set; }
        /// <summary>
        /// Original XML-Request der Apotheke (nur Rezept-XML) - zur Weiterleitung ins ARZ
        /// </summary>
        public string XmlRequest { get; set; } = null!;

        public virtual ErSenderezepteEmuster16? IdSenderezepteEmuster16Navigation { get; set; }
    }
}
