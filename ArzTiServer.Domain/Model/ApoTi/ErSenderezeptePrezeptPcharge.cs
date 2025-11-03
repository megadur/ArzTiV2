using System;
using System.Collections.Generic;
using ArzTi3Server.Domain.Model.ApoTi;

namespace ArzTi3Server.Domain.Model.ApoTi
{
    public partial class ErSenderezeptePrezeptPcharge
    {
        public ErSenderezeptePrezeptPcharge()
        {
            ErSenderezeptePrezeptPchargePwirkstoffs = new HashSet<ErSenderezeptePrezeptPchargePwirkstoff>();
        }

        public int IdSenderezeptePrezeptPcharge { get; set; }
        public int? IdSenderezeptePrezept { get; set; }
        /// <summary>
        /// herstellerSchluessel - Werte 1|2|3|4
        /// </summary>
        public short? HerstellerSchluessel { get; set; }
        /// <summary>
        /// herstellerNr - IK der Apotheke
        /// </summary>
        public long? HerstellerNr { get; set; }
        /// <summary>
        /// herstellungsDatum Format 2011-08-19T11:37:04.000
        /// </summary>
        public string? HerstellungsDatum { get; set; }
        /// <summary>
        /// chargenNr - Werte 1-99
        /// </summary>
        public short? ChargenNr { get; set; }
        /// <summary>
        /// anzahlApplikationen - Werte 1-99
        /// </summary>
        public short? AnzahlApplikationen { get; set; }

        public virtual ErSenderezeptePrezept? IdSenderezeptePrezeptNavigation { get; set; }
        public virtual ICollection<ErSenderezeptePrezeptPchargePwirkstoff> ErSenderezeptePrezeptPchargePwirkstoffs { get; set; }
    }
}
